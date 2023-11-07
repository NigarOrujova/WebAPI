using Application.Abstracts.Common.Interfaces;
using MediatR;

namespace Application.MetaDatas.Contacts.Queries;

public record ContactLanguageQuery:IRequest<object>;
internal class ContactLanguageQueryHandler : IRequestHandler<ContactLanguageQuery,object>
{
    private readonly IUnitOfWork _unitOfWork;

    public ContactLanguageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(ContactLanguageQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.ContactRepository.GetAsync()
            ?? throw new NullReferenceException();
        var data = new
        {
            contact_eng = new
            {
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
                MetaTitle= entity.MetaTitleAz,
                OgTitle=entity.OgTitleAz,
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