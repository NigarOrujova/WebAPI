using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

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
        IEnumerable<Portfolio> Portfolios = await _unitOfWork.PortfolioRepository.GetAllAsync(
           includes: new Expression<Func<Portfolio, object>>[]
           {
                x => x.PortfolioCategories,
                x => x.Images
           })
               ?? throw new NullReferenceException();

        IEnumerable<Category> Categories = await _unitOfWork.CategoryRepository.GetAllAsync(includes: x => x.PortfolioCategories)
            ?? throw new NullReferenceException();
        var data = new
        {
            category_en = Categories.Select(c =>new
            {
                c.Id,
                c.Name,
                portfolioCat = c.PortfolioCategories?.Select(x => new
                {
                    x.Portfolio.Id,
                    Title = x.Portfolio.Title ?? "",
                    SubTitle = x.Portfolio.SubTitle ?? "",
                    Description = x.Portfolio.Description ?? "",
                    x.Portfolio.IsMain,
                    x.Portfolio.Slug,
                    x.Portfolio.MetaKeyword,
                    x.Portfolio.MetaTitle,
                    x.Portfolio.OgTitle,
                    x.Portfolio.MetaDescription,
                    x.Portfolio.OgDescription,
                    x.Portfolio.MobileTitle,
                    portfolioCat = x.Portfolio.PortfolioCategories?.Select(x => x.Category.Name),
                    portfolioImg = x.Portfolio.Images?.Select(y => new
                    {
                        y.ImagePath,
                        y.ImageAlt,
                        y.IsMain
                    })
                }),
            }).ToList(),
            category_az = Categories.Select(c=>new 
            {
                c.Id,
                Name=c.NameAz, 
                portfolioCat = c.PortfolioCategories?.Select(x => new
                {
                    x.Portfolio.Id,
                    Title = x.Portfolio.TitleAz ?? "",
                    SubTitle = x.Portfolio.SubTitleAz ?? "",
                    Description = x.Portfolio.DescriptionAz ?? "",
                    x.Portfolio.IsMain,
                    x.Portfolio.Slug,
                    x.Portfolio.MetaKeywordAz,
                    x.Portfolio.MetaTitleAz,
                    x.Portfolio.OgTitleAz,
                    x.Portfolio.MetaDescriptionAz,
                    x.Portfolio.OgDescriptionAz,
                    x.Portfolio.MobileTitleAz,
                    portfolioCat = x.Portfolio.PortfolioCategories?.Select(x => x.Category.NameAz),
                    portfolioImg = x.Portfolio.Images?.Select(y => new
                    {
                        y.ImagePath,
                        y.ImageAltAz,
                        y.IsMain
                    })
                }),
            }).ToList()
        };
        return data;
    }
}
