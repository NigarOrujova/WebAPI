using Application.Abstracts.Common.Interfaces;
using Application.Dtos.Images;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Application.Portfolios.Commands.CreatePortfolio;

public record CreatePortfolioCommand : IRequest<int>
{
    public string Title { get; init; } = null!;
    public string TitleAz { get; init; } = null!;
    public string? SubTitle { get; init; }
    public string? SubTitleAz { get; init; }
    public string? Description { get; init; }
    public string? DescriptionAz { get; init; }
    public bool IsMain { get; init; }
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
    public string? MobileTitle { get; set; }
    public string? MobileTitleAz { get; set; }
    public List<ImageDto>? Images { get; set; }
    public List<int>? CategoryIds { get; set; } = new List<int>();
}
public class CreatePortfolioCommandHandler : IRequestHandler<CreatePortfolioCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostEnvironment _env;

    public CreatePortfolioCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }

    public async Task<int> Handle(CreatePortfolioCommand request, CancellationToken cancellationToken)
    {
        var entity = new Portfolio();

        entity.Title = request.Title;
        entity.TitleAz = request.TitleAz;
        entity.SubTitle = request.SubTitle;
        entity.SubTitleAz = request.SubTitleAz;
        entity.Description = request.Description;
        entity.DescriptionAz = request.DescriptionAz;
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
        entity.MobileTitle = request.MobileTitle;
        entity.MobileTitleAz = request.MobileTitleAz;
        entity.Slug = request.Title.ToSlug();
        entity.SlugAz = request.TitleAz.ToSlug();
        entity.IsMain = request.IsMain;
        if (request.CategoryIds != null)
        {
            entity.PortfolioCategories = new List<PortfolioCategory>();
            foreach (var id in request.CategoryIds)
            {
                PortfolioCategory portfolioCategory = new PortfolioCategory()
                {
                    CategoryId = id,
                    Portfolio = entity
                };
                entity.PortfolioCategories.Add(portfolioCategory);
            }
        }
        entity.Images = new List<PortfolioImage>();
        if (request.Images != null)
        {
            foreach (var item in request.Images)
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
        await _unitOfWork.PortfolioRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}