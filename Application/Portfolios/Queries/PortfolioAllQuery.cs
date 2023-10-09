using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Portfolios.Queries;

public record PortfolioAllQuery:IRequest<IEnumerable<Portfolio>>;
public class PortfolioAllQueryHandler : IRequestHandler<PortfolioAllQuery, IEnumerable<Portfolio>>
{
    private readonly IUnitOfWork _unitOfWork;

    public PortfolioAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Portfolio>> Handle(PortfolioAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Portfolio> Portfolios = await _unitOfWork.PortfolioRepository.GetAllAsync()
            ?? throw new NullReferenceException();

        return Portfolios;
    }
}
