using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

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
        IEnumerable<Portfolio> Portfolios = await _unitOfWork.PortfolioRepository.GetAllAsync(
        includes: new Expression<Func<Portfolio, object>>[]
        {
            x => x.PortfolioCategories,
            x => x.Images
        })
            ?? throw new NullReferenceException();

        IEnumerable<Category> categories = await _unitOfWork.CategoryRepository.GetAllAsync(includes: x => x.PortfolioCategories)
           ?? throw new NullReferenceException();

        return Portfolios;
    }
}
