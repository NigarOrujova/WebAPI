﻿using Application.Abstracts.Common.Exceptions;
using Application.Abstracts.Common.Interfaces;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Application.MetaDatas.Blogs.Commands;

public class BlogMetaUpdateCommand : IRequest<BlogMeta>
{
    public IFormFile? Image { get; set; }
    public string? ImageAlt { get; set; }
    public string? ImageAltAz { get; set; }
    public string? MetaKeyword { get; set; }
    public string? MetaKeywordAz { get; set; }
    public string? MetaTitle { get; set; }
    public string? MetaTitleAz { get; set; }
    public string? OgTitle { get; set; }
    public string? OgTitleAz { get; set; }
    public string? MetaDescription { get; set; }
    public string? MetaDescriptionAz { get; set; }
    public string? OgDescription { get; set; }
    public string? OgDescriptionAz { get; set; }
    public string? OgSiteName { get; set; }
    public string? OgSiteNameAz { get; set; }
    public string? MobileTitle { get; set; }
    public string? MobileTitleAz { get; set; }
    public string? AppName { get; set; }
    public string? AppNameAz { get; set; }
}
public class BlogMetaUpdateCommandHandler : IRequestHandler<BlogMetaUpdateCommand, BlogMeta>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostEnvironment _env;

    public BlogMetaUpdateCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }
    public async Task<BlogMeta> Handle(BlogMetaUpdateCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.BlogMetaRepository.GetAsync()
        ?? throw new NotImplementedException();

        if (request.Image == null)
        {
            request.Image = entity.Image;
            goto save;
        }

        if (!request.Image.CheckFileSize(1000))
            throw new FileException("File max size 1 mb");
        if (!request.Image.CheckFileType("image/"))
            throw new FileException("File type must be image");
        string newImageName = request.Image.GetRandomImagePath("home");

        _env.ArchiveImage(entity.ImagePath);
        await _env.SaveAsync(request.Image, newImageName, cancellationToken);

        entity.ImagePath = newImageName;

    save:
        entity.ImageAlt = request.ImageAlt;
        entity.ImageAltAz = request.ImageAltAz;
        entity.MetaKeyword = request.MetaKeyword;
        entity.MetaKeywordAz = request.MetaKeywordAz;
        entity.MetaTitle = request.MetaTitle;
        entity.MetaTitleAz = request.MetaTitleAz;
        entity.OgTitle = request.OgTitle;
        entity.OgTitleAz = request.OgTitleAz;
        entity.MetaDescription = request.MetaDescription;
        entity.MetaDescriptionAz = request.MetaDescriptionAz;
        entity.OgDescription = request.OgDescription;
        entity.OgDescriptionAz = request.OgDescriptionAz;
        entity.OgSiteName = request.OgSiteName;
        entity.OgSiteNameAz = request.OgSiteNameAz;
        entity.MobileTitle = request.MobileTitle;
        entity.MobileTitleAz = request.MobileTitleAz;
        entity.AppName = request.AppName;
        entity.AppNameAz = request.AppNameAz;

        await _unitOfWork.BlogMetaRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return entity;
    }
}