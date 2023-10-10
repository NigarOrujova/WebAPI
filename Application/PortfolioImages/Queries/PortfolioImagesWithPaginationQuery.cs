using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.PortfolioImages.Queries;

public class PortfolioImagesWithPaginationQuery:IRequest<List<PortfolioImage>>
{
    public int Page { get; set; }
    public int Size { get; set; }
}
public class PortfolioImagesWithPaginationQueryHandler:IRequestHandler<PortfolioImagesWithPaginationQuery, List<PortfolioImage>>
{
    private readonly IUnitOfWork _unitOfWork;

    public PortfolioImagesWithPaginationQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<PortfolioImage>> Handle(PortfolioImagesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return (List<PortfolioImage>)await _unitOfWork.PortfolioImageRepository.GetPaginatedAsync(request.Page,request.Size);
    }
}
