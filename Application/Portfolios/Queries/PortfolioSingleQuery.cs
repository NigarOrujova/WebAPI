using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Portfolios.Queries;

public record PortfolioSingleQuery(int Id) : IRequest<Portfolio>;

internal class PortfolioSingleQueryHandler : IRequestHandler<PortfolioSingleQuery, Portfolio>
{
    private readonly IUnitOfWork _unitOfWork;

    public PortfolioSingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Portfolio> Handle(PortfolioSingleQuery request, CancellationToken cancellationToken)
    {
        Portfolio entity = await _unitOfWork.PortfolioRepository.GetAsync(n => n.Id == request.Id,
            includes: new Expression<Func<Portfolio, object>>[]
            {
                x => x.PortfolioCategories,
                x => x.Images
            })
            ?? throw new NullReferenceException();

        return entity;
    }
}