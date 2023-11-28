using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Tags.Queries;

public record TagLanguageAllQuery : IRequest<object>;
public class TagLanguageAllQueryHandler : IRequestHandler<TagLanguageAllQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public TagLanguageAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(TagLanguageAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Tag> Tags = await _unitOfWork.TagRepository.GetAllAsync(
        includes:  x => x.TagCloud)
            ?? throw new NullReferenceException();

        IEnumerable<Blog> Blogs = await _unitOfWork.BlogRepository.GetAllAsync(
        includes: x => x.TagCloud)
            ?? throw new NullReferenceException();

        var data = new
        {
            Tag_en = Tags.Select(p => new
            {
                p.Id,
                Name = p.Name ?? "",
                BlogCat = p.TagCloud?.Select(x => new
                {
                    x.BlogId,
                    Title = x.Blog.Title ?? "",
                    Description = x.Blog.Description ?? "",
                    x.Blog.ImagePath,
                    x.Blog.Slug,
                    x.Blog.MetaKeyword,
                    x.Blog.MetaTitle,
                    x.Blog.OgTitle,
                    x.Blog.MetaDescription,
                    x.Blog.OgDescription,
                    x.Blog.MobileTitle,
                    PublishDate = x.Blog.PublishDate?.ToString("MMMM dd, yyyy") ?? "",
                    BlogCat = x.Blog.TagCloud?.Select(x => x.Tag.Name)
                })
            }),
            Tag_az = Tags.Select(p => new
            {
                p.Id,
                Name = p.NameAz,
                BlogCat = p.TagCloud?.Select(x => new
                {
                    x.BlogId,
                    Title = x.Blog.TitleAz ?? "",
                    Description = x.Blog.DescriptionAz ?? "",
                    x.Blog.ImagePath,
                    x.Blog.Slug,
                    x.Blog.MetaKeywordAz,
                    x.Blog.MetaTitleAz,
                    x.Blog.OgTitleAz,
                    x.Blog.MetaDescriptionAz,
                    x.Blog.OgDescriptionAz,
                    x.Blog.MobileTitleAz,
                    PublishDate = x.Blog.PublishDate?.ToString("MMMM dd, yyyy") ?? "",
                    BlogCat = x.Blog.TagCloud?.Select(x => x.Tag.NameAz)
                })
            })
        };

        return data;
    }
}