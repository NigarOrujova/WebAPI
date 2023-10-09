using Application.Abstracts.Common;
using Domain.Entities;
using MediatR;

namespace Application.Categorys.Queries;

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
        Category entity = await _unitOfWork.CategoryRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();

        return entity;
    }
}