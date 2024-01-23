using Application.Abstracts.Common.Exceptions;
using Application.Abstracts.Common.Interfaces;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Application.PortfolioImages.Commands.DeletePortfolioImage;

public record DeletePortfolioImageCommand(int Id) : IRequest<bool>;

internal class DeletePortfolioImageCommandHandler : IRequestHandler<DeletePortfolioImageCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostEnvironment _env;

    public DeletePortfolioImageCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }

    public async Task<bool> Handle(DeletePortfolioImageCommand request, CancellationToken cancellationToken)
    {
        PortfolioImage PortfolioImage = await _unitOfWork.PortfolioImageRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new FileException(message:"Image Not Found");
        if(PortfolioImage.IsMain)
        {
            throw new FileException(message:"Main image cannot be deleted");
        }
        if(PortfolioImage.ImagePath!= null)
        {
            _env.ArchiveImage(PortfolioImage.ImagePath);
        }
        await _unitOfWork.PortfolioImageRepository.DeleteAsync(PortfolioImage);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
