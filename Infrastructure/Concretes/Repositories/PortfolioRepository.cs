using Application.Abstracts.Repositories;
using Domain.Entities;
using Infrastructure.Concretes.Repositories.Base;
using Infrastructure.Persistance;
using System.Linq.Expressions;

namespace Infrastructure.Concretes.Repositories;

public class PortfolioRepository : Repository<Portfolio>, IPortfolioRepository
{
    public PortfolioRepository(YelloadDbContext context) : base(context) { }

    public async Task<object> GetPortfolioBySlugAsync(string slug)
    {
        var entity=await GetAsync(p => p.Slug.Equals(slug), includes: new Expression<Func<Portfolio, object>>[]
        {
            x => x.PortfolioCategories,
            x => x.Images
        });
        var data = new
        {
            portfolio_en = new
            {
                Title = entity.Title ?? "",
                SubTitle = entity.SubTitle ?? "",
                Description = entity.Description ?? "",
                entity.IsMain,
                entity.Slug,
                entity.MetaKeyword,
                entity.MetaTitle,
                entity.OgTitle,
                entity.MetaDescription,
                entity.OgDescription,
                entity.MobileTitle,
                portfolioCat = entity.PortfolioCategories?.Where(x => x != null && x.CategoryId != 0).Select(x => x.CategoryId),
                portfolioImg = entity.Images?.Select(y => new
                {
                    y.ImagePath,
                    y.ImageAlt,
                    y.IsMain
                })
            },
            portfolio_az = new
            {
                Title = entity.TitleAz,
                Description = entity.DescriptionAz,
                SubTitle = entity.SubTitleAz,
                Slug = entity.Slug,
                IsMain = entity.IsMain,
                MetaKeyword = entity.MetaKeywordAz,
                MetaTitle = entity.MetaTitleAz,
                OgTitle = entity.OgTitleAz,
                MetaDescription = entity.MetaDescriptionAz,
                OgDescription = entity.OgDescriptionAz,
                MobileTitle = entity.MobileTitleAz,
                portfolioCat = entity.PortfolioCategories?.Where(x => x != null && x.CategoryId != 0).Select(x => x.CategoryId),
                portfolioImg = entity.Images?.Select(y => new
                {
                    y.ImagePath,
                    y.ImageAlt,
                    y.IsMain
                })
            }
        };
        return data;
    }
}