using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Portfolios.Queries;

public record PortfolioLanguageAllQuery : IRequest<object>;
public class PortfolioLanguageAllQueryHandler : IRequestHandler<PortfolioLanguageAllQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public PortfolioLanguageAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(PortfolioLanguageAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Portfolio> Portfolios = await _unitOfWork.PortfolioRepository.GetAllAsync(
        includes: new Expression<Func<Portfolio, object>>[]
        {
            x => x.PortfolioCategories,
            x => x.Images
        })
            ?? throw new NullReferenceException();

        var data = new
        {
            portfolio_en= Portfolios.Select(p => new
            {
                p.Id,
                Title = p.Title ?? "",
                SubTitle = p.SubTitle ?? "",
                Description = p.Description ?? "",
                p.IsMain,
                portfolioCat = p.PortfolioCategories?.Where(x => x != null && x.CategoryId != 0).Select(x => x.CategoryId),
                portfolioImg = p.Images?.Select(x => x.ImagePath)
            }),
            portfolio_az=Portfolios.Select(p => new
            {
                p.Id,
                Title = p.TitleAz ?? "",
                SubTitle = p.SubTitleAz ?? "",
                Description = p.DescriptionAz ?? "",
                p.IsMain,
                portfolioCat = p.PortfolioCategories?.Where(x => x != null && x.CategoryId != 0).Select(x => x.CategoryId),
                portfolioImg = p.Images?.Select(x => x.ImagePath)
            })
        };

        return data;
    }
}