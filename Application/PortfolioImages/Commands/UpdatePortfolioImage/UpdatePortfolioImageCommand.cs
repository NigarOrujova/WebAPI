using Application.Abstracts.Common;
using Application.Extensions;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Application.PortfolioImages.Commands.UpdatePortfolioImage;

public record UpdatePortfolioImageCommand : IRequest<PortfolioImage>
{
    public int Id { get; init; }
    public IFormFile Image { get; set; }
    public string ImageAlt { get; init; }
    public bool IsMain { get; init; }
    public int PortfolioId { get; init; }
}
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
             ?? throw new NullReferenceException();
        if (request.Image == null)
        {
            request.Image = entity.Image;
            goto save;
        }

        if (!request.Image.CheckFileSize(1000))
            throw new FileException("File max size 1 mb");
        if (!request.Image.CheckFileType("image/"))
            throw new FileException("File type must be image");
        string newImageName = request.Image.GetRandomImagePath("customer");

        _env.ArchiveImage(entity.ImagePath);
        await _env.SaveAsync(request.Image, newImageName, cancellationToken);

        entity.ImagePath = newImageName;

    save:
        entity.ImageAlt = request.ImageAlt;
        entity.IsMain = request.IsMain;
        entity.PortfolioId = request.PortfolioId;

        await _unitOfWork.PortfolioImageRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}