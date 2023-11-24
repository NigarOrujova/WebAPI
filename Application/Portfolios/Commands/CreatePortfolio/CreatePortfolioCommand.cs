using Application.Abstracts.Common.Exceptions;
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
    public string? MetaKeyword { get; init; }
    public string? MetaKeywordAz { get; init; }
    public string? MetaTitle { get; init; }
    public string? MetaTitleAz { get; init; }
    public string? OgTitle { get; init; }
    public string? OgTitleAz { get; init; }
    public string? MetaDescription { get; init; }
    public string? MetaDescriptionAz { get; init; }
    public string? OgDescription { get; init; }
    public string? OgDescriptionAz { get; init; }
    public string? MobileTitle { get; init; }
    public string? MobileTitleAz { get; init; }
    public List<ImageDto>? Images { get; init; }
    public List<int>? CategoryIds { get; init; } = new List<int>();
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
        if (await _unitOfWork.PortfolioRepository.IsExistAsync(x => x.Title == request.Title))
            throw new FileException("Portfolio with the same title already exists.");

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