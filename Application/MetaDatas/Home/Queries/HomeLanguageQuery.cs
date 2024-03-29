﻿using Application.Abstracts.Common.Interfaces;
using MediatR;

namespace Application.MetaDatas.Home.Queries;

public record HomeLanguageQuery:IRequest<object>;
internal class HomeLanguageQueryHandler : IRequestHandler<HomeLanguageQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public HomeLanguageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(HomeLanguageQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.HomeRepository.GetAsync()
            ?? throw new NullReferenceException();
        var data = new
        {
            contact_en = new
            {
                entity.Title,
                entity.SubTitle,
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
            contact_az = new
            {
                Title=entity.TitleAz,
                SubTitle=entity.SubTitleAz,
                entity.ImagePath,
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
