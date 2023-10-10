using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Categories.Queries;

public class CategoriesWithPaginationQuery:IRequest<List<Category>>
{
    public int Page { get; set; }
    public int Size { get; set; }
}
public class CategoriesWithPaginationQueryHandler:IRequestHandler<CategoriesWithPaginationQuery, List<Category>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoriesWithPaginationQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Category>> Handle(CategoriesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return (List<Category>)await _unitOfWork.CategoryRepository.GetPaginatedAsync(request.Page,request.Size);
    }
}
