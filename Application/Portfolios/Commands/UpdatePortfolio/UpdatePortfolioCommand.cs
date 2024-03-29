﻿using Application.Abstracts.Common.Exceptions;
using Application.Abstracts.Common.Interfaces;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;
using System.Linq.Expressions;

namespace Application.Portfolios.Commands.UpdatePortfolio;

public record UpdatePortfolioCommand(int Id,Portfolio Portfolio) : IRequest<Portfolio>;
public class UpdatePortfolioCommandHandler : IRequestHandler<UpdatePortfolioCommand, Portfolio>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostEnvironment _env;

    public UpdatePortfolioCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }
    public async Task<Portfolio> Handle(UpdatePortfolioCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.PortfolioRepository.IsExistAsync(x => x.Title == request.Portfolio.Title && x.Id!=request.Id))
            throw new FileException("Portfolio with the same title already exists.");

        Portfolio entity = await _unitOfWork.PortfolioRepository.GetAsync(n => n.Id == request.Id, includes: new Expression<Func<Portfolio, object>>[]
        {
            x => x.PortfolioCategories,
            x => x.Images
        });
        IEnumerable<PortfolioImage> PortfolioImages = await _unitOfWork.PortfolioImageRepository.GetAllAsync()
            ?? throw new NullReferenceException();
        if (entity == null)
            throw new FileException("Portfolio Not Fount");

        entity.Title = request.Portfolio.Title;
        entity.Link = request.Portfolio.Link;
        entity.Sound = request.Portfolio.Sound;
        entity.TitleAz = request.Portfolio.TitleAz;
        entity.SubTitle = request.Portfolio.SubTitle;
        entity.SubTitleAz = request.Portfolio.SubTitleAz;
        entity.Description = request.Portfolio.Description;
        entity.DescriptionAz = request.Portfolio.DescriptionAz;
        entity.IsMain = request.Portfolio.IsMain;
        entity.MetaKeyword = request.Portfolio.MetaKeyword;
        entity.MetaKeywordAz = request.Portfolio.MetaKeywordAz;
        entity.MetaTitle = request.Portfolio.MetaTitle;
        entity.MetaTitleAz = request.Portfolio.MetaTitleAz;
        entity.OgTitle = request.Portfolio.OgTitle;
        entity.OgTitleAz = request.Portfolio.OgTitleAz;
        entity.MetaDescription = request.Portfolio.MetaDescription;
        entity.MetaDescriptionAz = request.Portfolio.MetaDescriptionAz;
        entity.OgDescription = request.Portfolio.OgDescription;
        entity.OgDescriptionAz = request.Portfolio.OgDescriptionAz;
        entity.MobileTitle = request.Portfolio.MobileTitle;
        entity.MobileTitleAz = request.Portfolio.MobileTitleAz;
        entity.Slug = entity.Title.ToSlug();
        entity.SlugAz = entity.TitleAz.ToSlug();
        if (request.Portfolio.CategoryIds != null)
        {
            if (entity.PortfolioCategories == null)
            {
                entity.PortfolioCategories = new List<PortfolioCategory>();
            }
            entity.PortfolioCategories?.RemoveAll(x => !request.Portfolio.CategoryIds.Contains(x.CategoryId));
            foreach (var categoryId in request.Portfolio.CategoryIds.Where(x => !entity.PortfolioCategories.Any(rc => rc.CategoryId == x)))
            {
                PortfolioCategory portfolioCategory = new PortfolioCategory
                {
                    PortfolioId = request.Id,
                    CategoryId = categoryId
                };
                entity.PortfolioCategories.Add(portfolioCategory);
            }
        }
        if (request.Portfolio.Images != null)
        {
            entity.Images = new List<PortfolioImage>();
            entity.Images?.RemoveAll(x => !request.Portfolio.ImageIds.Contains(x.Id));
            foreach (var item in request.Portfolio.Images)
            {
                if (item != null)
                {
                    if (!item.Image.CheckFileSize(1000))
                        throw new FileException("File max size 1 mb");
                    if (!item.Image.CheckFileType("image/"))
                        throw new FileException("File type must be image");
                    var image = new PortfolioImage();

                    image.ImagePath = item.Image.GetRandomImagePath("portfolio");

                    await _env.SaveAsync(item.Image, image.ImagePath, cancellationToken);

                    image.IsMain = item.IsMain;
                    image.ImageAlt = item.ImageAlt;
                    image.ImageAltAz = item.ImageAltAz;

                    entity.Images.Add(image);
                }
            }
        }
        else
        {
            entity.Images = entity.Images;
        }

        await _unitOfWork.PortfolioRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}