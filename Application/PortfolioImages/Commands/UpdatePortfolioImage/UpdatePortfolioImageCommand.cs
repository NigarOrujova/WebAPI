using Application.Abstracts.Common.Exceptions;
using Application.Abstracts.Common.Interfaces;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Application.PortfolioImages.Commands.UpdatePortfolioImage;

public record UpdatePortfolioImageCommand(int Id,PortfolioImage PortfolioImage) : IRequest<PortfolioImage>;
public class UpdatePortfolioImageCommandHandler : IRequestHandler<UpdatePortfolioImageCommand, PortfolioImage>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostEnvironment _env;

    public UpdatePortfolioImageCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }

    public async Task<PortfolioImage> Handle(UpdatePortfolioImageCommand request, CancellationToken cancellationToken)
    {
        PortfolioImage entity = await _unitOfWork.PortfolioImageRepository.GetAsync(n => n.Id == request.Id)
             ?? throw new NullReferenceException("Portfolio image not found");
        var portfolios = await _unitOfWork.PortfolioImageRepository.GetAllAsync(x => x.PortfolioId == request.PortfolioImage.PortfolioId);

        if (request.PortfolioImage.Image == null)
        {
            request.PortfolioImage.Image = entity.Image;
            goto save;
        }

        if (!request.PortfolioImage.Image.CheckFileSize(1000))
            throw new FileException("File max size 1 mb");
        if (!request.PortfolioImage.Image.CheckFileType("image/"))
            throw new FileException("File type must be image");
        string newImageName = request.PortfolioImage.Image.GetRandomImagePath("PortfolioImage");

        if (entity.ImagePath != null)
        {
            _env.ArchiveImage(entity.ImagePath);
        }
        await _env.SaveAsync(request.PortfolioImage.Image, newImageName, cancellationToken);

        entity.ImagePath = newImageName;

    save:
        entity.ImageAlt = request.PortfolioImage.ImageAlt;
        entity.ImageAltAz = request.PortfolioImage.ImageAltAz;
        entity.PortfolioId = request.PortfolioImage.PortfolioId;
        if (request.PortfolioImage.IsMain)
        {
            foreach (var item in portfolios)
            {
                item.IsMain = false;
            }
            entity.IsMain = request.PortfolioImage.IsMain;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        else
        {
            var count = 0;
            foreach (var portfolio in portfolios)
            {
                if (portfolio.IsMain) count++;
            }
            if (count != 1)
                throw new FileException("1 image must be isMain");
            entity.IsMain = request.PortfolioImage.IsMain;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        

        await _unitOfWork.PortfolioImageRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}