using Application.Abstracts.Common.Interfaces;
using Application.MetaDatas.Love.Queries;
using MediatR;

namespace Application.MetaDatas.We.Queries;

public record WeLanguageQuery:IRequest<object>;

internal class WeLanguageQueryHandler : IRequestHandler<WeLanguageQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public WeLanguageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(WeLanguageQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.HomeRepository.GetAsync()
            ?? throw new NullReferenceException();
        var data = new
        {
            contact_eng = new
            {
                entity.MetaKeyword,
                entity.MetaTitle,
                entity.OgTitle,
                entity.MetaDescription,
                entity.OgDescription,
                entity.OgSiteName,
                entity.MobileTitle,
                entity.AppName
            },
            contact_aze = new
            {
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