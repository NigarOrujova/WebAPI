using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Portfolios.Queries;

public record PortfolioLanguageQuery(int Id):IRequest<object>;
internal class PortfolioLanguageQueryHandler : IRequestHandler<PortfolioLanguageQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public PortfolioLanguageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(PortfolioLanguageQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Portfolio, object>>[] includes = new Expression<Func<Portfolio, object>>[]
        {
            x => x.PortfolioCategories,
            x => x.Images
        };

        Portfolio entity = await _unitOfWork.PortfolioRepository.GetAsync(
            n => n.Id == request.Id,
            includes: includes
        ) ?? throw new NullReferenceException();

        var data = new
        {
            portfolio_en = new
            {
                Title=entity.Title ?? "",
                SubTitle=entity.SubTitle ?? "",
                Description = entity.Description ?? "",
                entity.IsMain,
                portfolioCat = entity.PortfolioCategories?.Where(x => x != null && x.CategoryId != 0).Select(x => x.CategoryId),
                portfolioImg=entity.Images?.Select(x=>x.ImagePath)
            },
            portfolio_az = new
            {
                Title = entity.TitleAz ?? "",
                SubTitle = entity.SubTitleAz ?? "",
                Description = entity.DescriptionAz ?? "",
                entity.IsMain,
                portfolioCat = entity.PortfolioCategories?.Where(x => x != null && x.CategoryId != 0).Select(x => x.CategoryId),
                portfolioImg = entity.Images?.Select(x => x.ImagePath)
            }
        };
        return data;
    }
}