using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Categories.Queries;

public record CategoryAllQuery:IRequest<IEnumerable<Category>>;
public class CategoryAllQueryHandler : IRequestHandler<CategoryAllQuery, IEnumerable<Category>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Category>> Handle(CategoryAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Portfolio> Portfolios = await _unitOfWork.PortfolioRepository.GetAllAsync(
          includes: new Expression<Func<Portfolio, object>>[]
          {
                x => x.PortfolioCategories,
                x => x.Images
          })
              ?? throw new NullReferenceException();

        IEnumerable<Category> Categories = await _unitOfWork.CategoryRepository.GetAllAsync(includes:x=>x.PortfolioCategories)
            ?? throw new NullReferenceException();

        return Categories;
    }
}
