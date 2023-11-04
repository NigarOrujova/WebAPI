using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

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
            ?? throw new NullReferenceException();

        return entity;
    }
}