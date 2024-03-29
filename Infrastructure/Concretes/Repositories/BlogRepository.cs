﻿using Application.Abstracts.Repositories;
using Domain.Entities;
using Infrastructure.Concretes.Repositories.Base;
using Infrastructure.Persistance;
using System.Linq.Expressions;

namespace Infrastructure.Concretes.Repositories;

public class BlogRepository : Repository<Blog>, IBlogRepository
{
    public BlogRepository(YelloadDbContext context) : base(context) { }

    public async Task<object> GetBlogBySlugAsync(string slug)
    {
        var entity = await GetAsync(p => p.Slug.Equals(slug), includes: x => x.TagCloud);

        var data = new
        {
            portfolio_en = new
            {
                Title = entity.Title ?? "",
                Description = entity.Description ?? "",
                entity.Slug,
                entity.ImagePath,
                entity.MetaKeyword,
                entity.MetaTitle,
                entity.OgTitle,
                entity.MetaDescription,
                entity.OgDescription,
                entity.MobileTitle,
                PublishDate = entity.PublishDate?.ToString("MMMM dd, yyyy") ?? "",
                portfolioCat = entity.TagCloud?.Where(x => x != null && x.TagId != 0).Select(x => x.TagId)
            },
            portfolio_az = new
            {
                Title = entity.TitleAz,
                Description = entity.DescriptionAz,
                Slug = entity.Slug,
                entity.ImagePath,
                MetaKeyword = entity.MetaKeywordAz,
                MetaTitle = entity.MetaTitleAz,
                OgTitle = entity.OgTitleAz,
                MetaDescription = entity.MetaDescriptionAz,
                OgDescription = entity.OgDescriptionAz,
                MobileTitle = entity.MobileTitleAz,
                PublishDate = entity.PublishDate?.ToString("MMMM dd, yyyy") ?? "",
                portfolioCat = entity.TagCloud?.Where(x => x != null && x.TagId != 0).Select(x => x.TagId)
            }
        };
        return data;
    }
}
