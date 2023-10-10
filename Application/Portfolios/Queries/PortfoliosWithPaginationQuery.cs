using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Portfolios.Queries;

public class PortfoliosWithPaginationQuery:IRequest<List<Portfolio>>
{
    public int Page { get; set; }
    public int Size { get; set; }
}
public class PortfoliosWithPaginationQueryHandler:IRequestHandler<PortfoliosWithPaginationQuery, List<Portfolio>>
{
    private readonly IUnitOfWork _unitOfWork;

    public PortfoliosWithPaginationQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Portfolio>> Handle(PortfoliosWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return (List<Portfolio>)await _unitOfWork.PortfolioRepository.GetPaginatedAsync(request.Page,request.Size);
    }
}
