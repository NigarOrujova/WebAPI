using Application.Abstracts.Common.Interfaces;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;

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
        Portfolio entity = await _unitOfWork.PortfolioRepository.GetAsync(n => n.Id == request.Id);
        if (entity == null)
        {
            return null;
        }
        entity.Title = request.Portfolio.Title;
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
        entity.Images = new List<PortfolioImage>();
        if (request.Portfolio.Images != null)
        {
            foreach (var item in request.Portfolio.Images)
            {
                if (item != null)
                {
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
        await _unitOfWork.PortfolioRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}