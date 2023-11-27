using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Categories.Queries;

public record CategorySingleQuery(int Id) : IRequest<Category>;

internal class CategorySingleQueryHandler : IRequestHandler<CategorySingleQuery, Category>
{
    private readonly IUnitOfWork _unitOfWork;

    public CategorySingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Category> Handle(CategorySingleQuery request, CancellationToken cancellationToken)
    {

        IEnumerable<Portfolio> Portfolios = await _unitOfWork.PortfolioRepository.GetAllAsync(
          includes: new Expression<Func<Portfolio, object>>[]
          {
                x => x.PortfolioCategories,
                x => x.Images
          })
              ?? throw new NullReferenceException();

        Category entity = await _unitOfWork.CategoryRepository.GetAsync(n => n.Id == request.Id,includes: x => x.PortfolioCategories)
            ?? throw new NullReferenceException();

        return entity;
    }
}