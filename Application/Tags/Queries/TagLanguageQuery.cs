using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Tags.Queries;

public record TagLanguageQuery(int Id) : IRequest<object>;
internal class TagLanguageQueryHandler : IRequestHandler<TagLanguageQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public TagLanguageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(TagLanguageQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Tag, object>>[] includes = new Expression<Func<Tag, object>>[]
        {
            x => x.TagCloud
        };

        Tag entity = await _unitOfWork.TagRepository.GetAsync(
            n => n.Id == request.Id,
            includes: includes
        ) ?? throw new NullReferenceException();

        IEnumerable<Blog> Blogs = await _unitOfWork.BlogRepository.GetAllAsync(
        includes: x => x.TagCloud)
            ?? throw new NullReferenceException();

        var data = new
        {
            Tag_en = new
            {
                Name = entity.Name ?? "",
                BlogCat = entity.TagCloud?.Select(x => new
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
            },
            Tag_az = new
            {
                Name = entity.NameAz,
                BlogCat = entity.TagCloud?.Select(x => new
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
                }),
            }
        };
        return data;
    }
}