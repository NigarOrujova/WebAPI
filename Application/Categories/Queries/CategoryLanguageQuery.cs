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
        IEnumerable<Portfolio> Portfolios = await _unitOfWork.PortfolioRepository.GetAllAsync()
              ?? throw new NullReferenceException("portfolio is null");

        Category entity = await _unitOfWork.CategoryRepository.GetAsync(n => n.Id == request.Id, includes: x => x.PortfolioCategories)
            ?? throw new NullReferenceException("category is null");
        var data = new
        {
            category_en = new
            {
                entity.Name,
                portfolioCat = entity.PortfolioCategories?.Select(x => new
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
                })

            },
            category_az = new
            {
                Name=entity.NameAz,
                portfolioCat = entity.PortfolioCategories?.Select(x => new
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
                })
            }
        };
        return data;
    }
}
