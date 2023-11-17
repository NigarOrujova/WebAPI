using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Portfolios.Queries;

public class PortfolioLanguageWithPaginationQuery : IRequest<object>
{
    public int Page { get; set; }
    public int Size { get; set; }
    public int? CategoryId { get; set; }
}
public class PortfolioLanguageWithPaginationQueryHandler : IRequestHandler<PortfolioLanguageWithPaginationQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public PortfolioLanguageWithPaginationQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(PortfolioLanguageWithPaginationQuery request, CancellationToken cancellationToken)
    {
        int pageSize = request.Size;
        int pageNumber = request.Page;

        var totalCount = await _unitOfWork.PortfolioRepository.GetTotalCountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        IEnumerable<Portfolio> Portfolios = await _unitOfWork.PortfolioRepository.GetAllAsync(
        includes: new Expression<Func<Portfolio, object>>[]
        {
            x => x.PortfolioCategories,
            x => x.Images
        })
            ?? throw new NullReferenceException();

        if (pageNumber > totalPages)
        {
            pageNumber = totalPages;
        }
        else if (pageNumber < 1)
        {
            pageNumber = 1;
        }

        if (request.CategoryId > 0)
        {
            var filteredPortfolios = Portfolios
               .Where(x => x.PortfolioCategories.Any(y => y.CategoryId == request.CategoryId))
               .OrderByDescending(x => x.Id)
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToList();

            var model = new
            {
                project_en = filteredPortfolios
                .Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.SubTitle,
                    x.Description,
                    x.Slug,
                    x.IsMain,
                    x.MetaKeyword,
                    x.MetaTitle,
                    x.OgTitle,
                    x.MetaDescription,
                    x.OgDescription,
                    x.MobileTitle,
                    portfolioCat = x.PortfolioCategories?.Where(y => y != null && y.CategoryId != 0).Select(x => x.CategoryId),
                    portfolioImg = x.Images?.Select(y => new
                    {
                        y.ImagePath,
                        y.ImageAlt,
                        y.IsMain
                    })
                }).ToList(),
                project_az = filteredPortfolios
                .Select(x => new
                {
                    x.Id,
                    Title = x.TitleAz,
                    Description = x.DescriptionAz,
                    SubTitle = x.SubTitleAz,
                    Slug = x.Slug,
                    IsMain = x.IsMain,
                    MetaKeyword = x.MetaKeywordAz,
                    MetaTitle = x.MetaTitleAz,
                    OgTitle = x.OgTitleAz,
                    MetaDescription = x.MetaDescriptionAz,
                    OgDescription = x.OgDescriptionAz,
                    MobileTitle = x.MobileTitleAz,
                    portfolioCat = x.PortfolioCategories?.Where(y => y != null && y.CategoryId != 0).Select(x => x.CategoryId),
                    portfolioImg = x.Images?.Select(y => new
                    {
                        y.ImagePath,
                        y.ImageAlt,
                        y.IsMain
                    })
                }).ToList(),
                totalPages = totalPages
            };

            return model;
        }
        else
        {
            var model = new
            {
                project_en = Portfolios
                .OrderByDescending(x => x.Id)
                .Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.SubTitle,
                    x.Description,
                    x.Slug,
                    x.IsMain,
                    x.MetaKeyword,
                    x.MetaTitle,
                    x.OgTitle,
                    x.MetaDescription,
                    x.OgDescription,
                    x.MobileTitle,
                    portfolioCat = x.PortfolioCategories?.Where(y => y != null && y.CategoryId != 0).Select(x => x.CategoryId),
                    portfolioImg = x.Images?.Select(y => new
                    {
                        y.ImagePath,
                        y.ImageAlt,
                        y.IsMain
                    })
                }).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(),
                project_az = Portfolios
                .OrderByDescending(x => x.Id)
                .Select(x => new
                {
                    x.Id,
                    Title = x.TitleAz,
                    Description = x.DescriptionAz,
                    SubTitle = x.SubTitleAz,
                    Slug = x.Slug,
                    IsMain = x.IsMain,
                    MetaKeyword = x.MetaKeywordAz,
                    MetaTitle = x.MetaTitleAz,
                    OgTitle = x.OgTitleAz,
                    MetaDescription = x.MetaDescriptionAz,
                    OgDescription = x.OgDescriptionAz,
                    MobileTitle = x.MobileTitleAz,
                    portfolioCat = x.PortfolioCategories?.Where(y => y != null && y.CategoryId != 0).Select(x => x.CategoryId),
                    portfolioImg = x.Images?.Select(y => new
                    {
                        y.ImagePath,
                        y.ImageAlt,
                        y.IsMain
                    })
                }).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(),
                totalPages = totalPages
            };

            return model;
        }
    }
}