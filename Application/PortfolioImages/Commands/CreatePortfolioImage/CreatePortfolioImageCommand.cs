using Application.Abstracts.Common;
using Domain.Entities;
using MediatR;

namespace Application.PortfolioImages.Commands.CreatePortfolioImage;

public record CreatePortfolioImageCommand : IRequest<int>
{
    public string ImageAlt { get; init; } = null!;
    public bool IsMain { get; init; }
    public int PortfolioId { get; init; }
}
public class CreatePortfolioImageCommandHandler : IRequestHandler<CreatePortfolioImageCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePortfolioImageCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreatePortfolioImageCommand request, CancellationToken cancellationToken)
    {
        var entity = new PortfolioImage();

        entity.ImageAlt = request.ImageAlt;
        entity.IsMain = request.IsMain;
        entity.PortfolioId = request.PortfolioId;

        await _unitOfWork.PortfolioImageRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}