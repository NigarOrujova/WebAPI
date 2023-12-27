using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq;
using System.Linq.Expressions;

namespace Application.Portfolios.Queries;

public record PortfolioLanguageAllQuery : IRequest<object>;
public class PortfolioLanguageAllQueryHandler : IRequestHandler<PortfolioLanguageAllQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public PortfolioLanguageAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(PortfolioLanguageAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Portfolio> Portfolios = await _unitOfWork.PortfolioRepository.GetAllAsync(
        includes: new Expression<Func<Portfolio, object>>[]
        {
            x => x.PortfolioCategories,
            x => x.Images
        })
            ?? throw new NullReferenceException();

        IEnumerable<Category> categories = await _unitOfWork.CategoryRepository.GetAllAsync(includes:x=> x.PortfolioCategories)
           ?? throw new NullReferenceException();


        var data = new
        {
            portfolio_en= Portfolios.Select(p => new
            {
                p.Id,
                Title = p.Title ?? "",
                SubTitle = p.SubTitle ?? "",
                Description = p.Description ?? "",
                p.Link,
                p.Sound,
                p.IsMain,
                p.Slug,
                p.MetaKeyword,
                p.MetaTitle,
                p.OgTitle,
                p.MetaDescription,
                p.OgDescription,
                p.MobileTitle,
                portfolioCat = p.PortfolioCategories?.Select(x=>new
                {
                    CategoryId = x.CategoryId,
                    CategoryName=x.Category.Name
                }),
                portfolioImg = p.Images?.Select(y => new
                {
                    y.Id,
                    y.ImagePath,
                    y.ImageAlt,
                    y.IsMain
                })
            }),
            portfolio_az=Portfolios.Select(p => new
            {
                p.Id,
                Title = p.TitleAz,
                Description = p.DescriptionAz,
                SubTitle = p.SubTitleAz,
                p.Link,
                p.Sound,
                Slug = p.Slug,
                IsMain = p.IsMain,
                MetaKeyword = p.MetaKeywordAz,
                MetaTitle = p.MetaTitleAz,
                OgTitle = p.OgTitleAz,
                MetaDescription = p.MetaDescriptionAz,
                OgDescription = p.OgDescriptionAz,
                MobileTitle = p.MobileTitleAz,
                portfolioCat = p.PortfolioCategories?.Select(x => new
                {
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.NameAz
                }),
                portfolioImg = p.Images?.Select(y => new
                {
                    y.Id,
                    y.ImagePath,
                    ImageAlt=y.ImageAltAz,
                    y.IsMain
                })
            })
        };

        return data;
    }
}