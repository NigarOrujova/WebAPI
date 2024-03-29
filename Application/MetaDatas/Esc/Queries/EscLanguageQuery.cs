﻿using Application.Abstracts.Common.Interfaces;
using MediatR;

namespace Application.MetaDatas.Esc.Queries;

public record EscLanguageQuery:IRequest<object>;

internal class EscLanguageQueryHandler : IRequestHandler<EscLanguageQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public EscLanguageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(EscLanguageQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.EscRepository.GetAsync()
            ?? throw new NullReferenceException();
        var data = new
        {
            contact_en = new
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
            contact_az = new
            {
                MetaKeyword=entity.MetaKeywordAz,
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