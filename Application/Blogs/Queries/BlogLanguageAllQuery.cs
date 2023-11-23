using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Blogs.Queries;


public record BlogLanguageAllQuery : IRequest<object>;
public class BlogLanguageAllQueryHandler : IRequestHandler<BlogLanguageAllQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public BlogLanguageAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(BlogLanguageAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Blog> Blogs = await _unitOfWork.BlogRepository.GetAllAsync(
        includes: x => x.TagCloud)
            ?? throw new NullReferenceException();

        var data = new
        {
            Blog_en = Blogs.Select(p => new
            {
                p.Id,
                Title = p.Title ?? "",
                Description = p.Description ?? "",
                p.ImagePath,
                p.Slug,
                p.MetaKeyword,
                p.MetaTitle,
                p.OgTitle,
                p.MetaDescription,
                p.OgDescription,
                p.MobileTitle,
                PublishDate = p.PublishDate?.ToString("MMMM dd, yyyy") ?? "",
                BlogCat = p.TagCloud?.Where(x => x != null && x.TagId != 0).Select(x => x.TagId)
            }),
            Blog_az = Blogs.Select(p => new
            {
                p.Id,
                Title = p.TitleAz,
                Description = p.DescriptionAz,
                Slug = p.Slug,
                p.ImagePath,
                MetaKeyword = p.MetaKeywordAz,
                MetaTitle = p.MetaTitleAz,
                OgTitle = p.OgTitleAz,
                MetaDescription = p.MetaDescriptionAz,
                OgDescription = p.OgDescriptionAz,
                MobileTitle = p.MobileTitleAz,
                PublishDate = p.PublishDate?.ToString("MMMM dd, yyyy") ?? "",
                BlogCat = p.TagCloud?.Where(x => x != null && x.TagId != 0).Select(x => x.TagId)
            })
        };

        return data;
    }
}