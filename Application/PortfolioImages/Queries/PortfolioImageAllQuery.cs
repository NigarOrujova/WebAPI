using Application.Abstracts.Common;
using Domain.Entities;
using MediatR;

namespace Application.PortfolioImages.Queries;

public record PortfolioImageAllQuery:IRequest<IEnumerable<PortfolioImage>>;
public class PortfolioImageAllQueryHandler : IRequestHandler<PortfolioImageAllQuery, IEnumerable<PortfolioImage>>
{
    private readonly IUnitOfWork _unitOfWork;

    public PortfolioImageAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PortfolioImage>> Handle(PortfolioImageAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<PortfolioImage> PortfolioImages = await _unitOfWork.PortfolioImageRepository.GetAllAsync()
            ?? throw new NullReferenceException();

        return PortfolioImages;
    }
}
