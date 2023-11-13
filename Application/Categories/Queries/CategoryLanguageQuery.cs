using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Categories.Queries;

public record CategoryLanguageQuery(int Id) : IRequest<object>;

internal class CategoryLanguageQueryHandler : IRequestHandler<CategoryLanguageQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryLanguageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(CategoryLanguageQuery request, CancellationToken cancellationToken)
    {
        Category entity = await _unitOfWork.CategoryRepository.GetAsync(n => n.Id == request.Id, includes: x => x.PortfolioCategories)
            ?? throw new NullReferenceException();
        var data = new
        {
            category_en = new
            {
                entity.Name,
                portfolioCat = entity.PortfolioCategories?.Where(x => x != null && x.PortfolioId != 0).Select(x => x.PortfolioId),

            },
            category_az = new
            {
                Name=entity.NameAz,
                portfolioCat = entity.PortfolioCategories?.Where(x => x != null && x.PortfolioId != 0).Select(x => x.PortfolioId),
            }
        };
        return data;
    }
}
