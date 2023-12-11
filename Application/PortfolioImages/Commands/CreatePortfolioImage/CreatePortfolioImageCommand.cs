using Application.Abstracts.Common.Interfaces;
using Application.Extensions;
using Domain.Entities;
using Application.Abstracts.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Application.PortfolioImages.Commands.CreatePortfolioImage;

public record CreatePortfolioImageCommand : IRequest<int>
{
    public IFormFile Image { get; set; }
    public string ImageAlt { get; init; } = null!;
    public string ImageAltAz { get; init; }
    public bool IsMain { get; init; }
    public int PortfolioId { get; init; }
}
public class CreatePortfolioImageCommandHandler : IRequestHandler<CreatePortfolioImageCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostEnvironment _env;

    public CreatePortfolioImageCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }

    public async Task<int> Handle(CreatePortfolioImageCommand request, CancellationToken cancellationToken)
    {
        var entity = new PortfolioImage();
        if (request.Image != null)
        {
            if (!request.Image.CheckFileSize(1000))
                throw new FileException("File max size 1 mb");
            if (!request.Image.CheckFileType("image/"))
                throw new FileException("File type must be image");

            string newImageName = request.Image.GetRandomImagePath("PortfolioImage");
            await _env.SaveAsync(request.Image, newImageName, cancellationToken);
            entity.ImagePath = newImageName;
        }
        entity.ImageAlt = request.ImageAlt;
        entity.ImageAltAz = request.ImageAltAz;
        entity.IsMain = request.IsMain;
        entity.PortfolioId = request.PortfolioId;

        await _unitOfWork.PortfolioImageRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}