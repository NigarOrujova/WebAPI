using Application.Abstracts.Common.Interfaces;
using MediatR;

namespace Application.MetaDatas.Blogs.Queries;

public record BlogMetaLanguageQuery : IRequest<object>;

internal class BlogMetaLanguageQueryHandler : IRequestHandler<BlogMetaLanguageQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public BlogMetaLanguageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(BlogMetaLanguageQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.BlogMetaRepository.GetAsync()
            ?? throw new NullReferenceException();

        var data = new
        {
            blogmeta_en = new
            {
                entity.ImagePath,
                entity.ImageAlt,
                entity.MetaKeyword,
                entity.MetaTitle,
                entity.OgTitle,
                entity.MetaDescription,
                entity.OgDescription,
                entity.OgSiteName,
                entity.MobileTitle,
                entity.AppName
            },
            blogmeta_az = new
            {
                ImagePath=entity.ImagePath,
                ImageAlt=entity.ImageAltAz,
                MetaKeyword = entity.MetaKeywordAz,
                MetaTitle = entity.MetaTitleAz,
                OgTitle = entity.OgTitleAz,
                MetaDescription = entity.MetaDescriptionAz,
                OgDescription = entity.OgDescriptionAz,
                OgSiteName = entity.OgSiteNameAz,
                MobileTitle = entity.MobileTitleAz,
                AppName = entity.AppNameAz
            }
        };

        return data;
    }
}