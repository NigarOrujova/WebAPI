using Application.Abstracts.Common;
using Domain.Entities;
using MediatR;

namespace Application.PortfolioImages.Commands.DeletePortfolioImage;

public record DeletePortfolioImageCommand(int Id) : IRequest<bool>;

internal class DeletePortfolioImageCommandHandler : IRequestHandler<DeletePortfolioImageCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePortfolioImageCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeletePortfolioImageCommand request, CancellationToken cancellationToken)
    {
        PortfolioImage PortfolioImage = await _unitOfWork.PortfolioImageRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();

        await _unitOfWork.PortfolioImageRepository.DeleteAsync(PortfolioImage);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
