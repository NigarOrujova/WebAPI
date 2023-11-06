using Application.Abstracts.Common.Interfaces;
using Application.Dtos.Images;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Application.Portfolios.Commands.UpdatePortfolio;

public record UpdatePortfolioCommand(int Id,Portfolio Portfolio) : IRequest<Portfolio>
{
    public List<ImageDto>? Images { get; set; }
}
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
        Portfolio entity = await _unitOfWork.PortfolioRepository.GetAsync(n => n.Id == request.Id)
             ?? throw new NullReferenceException();

        entity.Title = request.Portfolio.Title;
        entity.SubTitle = request.Portfolio.SubTitle;
        entity.Description = request.Portfolio.Description;
        entity.IsMain = request.Portfolio.IsMain;
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

                    entity.Images.Add(image);
                }
            }
        }
        await _unitOfWork.PortfolioRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}