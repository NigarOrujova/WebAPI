using Application.Abstracts.Common;
using Domain.Entities;
using MediatR;

namespace Application.PortfolioImages.Commands.UpdatePortfolioImage;

public record UpdatePortfolioImageCommand : IRequest<PortfolioImage>
{
    public int Id { get; init; }
    public string ImageAlt { get; init; }
    public bool IsMain { get; init; }
    public int PortfolioId { get; init; }
}
public class UpdatePortfolioImageCommandHandler : IRequestHandler<UpdatePortfolioImageCommand, PortfolioImage>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePortfolioImageCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PortfolioImage> Handle(UpdatePortfolioImageCommand request, CancellationToken cancellationToken)
    {
        PortfolioImage entity = await _unitOfWork.PortfolioImageRepository.GetAsync(n => n.Id == request.Id)
             ?? throw new NullReferenceException();

        entity.ImageAlt = request.ImageAlt;
        entity.IsMain = request.IsMain;
        entity.PortfolioId = request.PortfolioId;

        await _unitOfWork.PortfolioImageRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}