using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Categories.Queries;

public record CategoryLanguageAllQuery : IRequest<object>;
public class CategoryLanguageAllQueryHandler : IRequestHandler<CategoryLanguageAllQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryLanguageAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(CategoryLanguageAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Category> Categories = await _unitOfWork.CategoryRepository.GetAllAsync(includes: x => x.PortfolioCategories)
            ?? throw new NullReferenceException();
        var data = new
        {
            category_en = Categories.Select(c =>new
            {
                c.Id,
                c.Name,
                portfolioCat = c.PortfolioCategories?.Select(x=>x.PortfolioId),
            }).ToList(),
            category_az = Categories.Select(c=>new 
            {
                c.Id,
                Name=c.NameAz, 
                portfolioCat = c.PortfolioCategories?.Select(x => x.PortfolioId),
            }).ToList()
        };
        return data;
    }
}
