using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Blogs.Queries;

public record BlogLanguageQuery(int Id) : IRequest<object>;
internal class BlogLanguageQueryHandler : IRequestHandler<BlogLanguageQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public BlogLanguageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(BlogLanguageQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Blog, object>>[] includes = new Expression<Func<Blog, object>>[]
        {
            x => x.TagCloud
        };

        Blog entity = await _unitOfWork.BlogRepository.GetAsync(
            n => n.Id == request.Id,
            includes: includes
        ) ?? throw new NullReferenceException();

        var data = new
        {
            Blog_en = new
            {
                Title = entity.Title ?? "",
                Description = entity.Description ?? "",
                entity.Slug,
                entity.MetaKeyword,
                entity.MetaTitle,
                entity.OgTitle,
                entity.MetaDescription,
                entity.OgDescription,
                entity.MobileTitle,
                BlogCat = entity.TagCloud?.Where(x => x != null && x.TagId != 0).Select(x => x.TagId)
            },
            Blog_az = new
            {
                Title = entity.TitleAz,
                Description = entity.DescriptionAz,
                Slug = entity.Slug,
                MetaKeyword = entity.MetaKeywordAz,
                MetaTitle = entity.MetaTitleAz,
                OgTitle = entity.OgTitleAz,
                MetaDescription = entity.MetaDescriptionAz,
                OgDescription = entity.OgDescriptionAz,
                MobileTitle = entity.MobileTitleAz,
                BlogCat = entity.TagCloud?.Where(x => x != null && x.TagId != 0).Select(x => x.TagId)
            }
        };
        return data;
    }
}