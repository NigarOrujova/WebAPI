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
        Category entity = await _unitOfWork.CategoryRepository.GetAsync(n => n.Id == request.Id,includes: x => x.PortfolioCategories)
            ?? throw new InvalidOperationException("Categories is null");

        IEnumerable<Portfolio> portfolio = await _unitOfWork.PortfolioRepository.GetAllAsync()
            ?? throw new InvalidOperationException("Portfolio entity is null");

        return entity;
    }
}