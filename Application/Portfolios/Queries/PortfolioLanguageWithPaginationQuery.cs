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

        IEnumerable<Portfolio> Portfolios = await _unitOfWork.PortfolioRepository.GetAllAsync(
        includes: new Expression<Func<Portfolio, object>>[]
        {
            x => x.PortfolioCategories,
            x => x.Images
        })
            ?? throw new InvalidOperationException("Portfolio entity is null");

        IEnumerable<Category> categories = await _unitOfWork.CategoryRepository.GetAllAsync(includes: x => x.PortfolioCategories)
            ?? throw new InvalidOperationException("Categories is null");

        if (request.CategoryId > 0)
        {
            var filteredPortfolios = Portfolios
               .Where(x => x.PortfolioCategories.Any(y => y.CategoryId == request.CategoryId))
               .OrderByDescending(x => x.Id)
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToList();
            var totalCount = Portfolios.Where(x => x.PortfolioCategories.Any(y => y.CategoryId == request.CategoryId)).Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            if (pageNumber > totalPages)
            {
                pageNumber = totalPages;
            }
            else if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            var model = new
            {
                project_en = filteredPortfolios
                .Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.SubTitle,
                    x.Description,
                    x.Link,
                    x.Sound,
                    x.Slug,
                    x.IsMain,
                    x.MetaKeyword,
                    x.MetaTitle,
                    x.OgTitle,
                    x.MetaDescription,
                    x.OgDescription,
                    x.MobileTitle,
                    portfolioCat = x.PortfolioCategories?.Select(x => new
                    {
                        CategoryId = x.CategoryId,
                        CategoryName = x.Category.Name
                    }),
                    portfolioImg = x.Images?.Select(y => new
                    {
                        y.Id,
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
                    x.Link,
                    x.Sound,
                    Slug = x.Slug,
                    IsMain = x.IsMain,
                    MetaKeyword = x.MetaKeywordAz,
                    MetaTitle = x.MetaTitleAz,
                    OgTitle = x.OgTitleAz,
                    MetaDescription = x.MetaDescriptionAz,
                    OgDescription = x.OgDescriptionAz,
                    MobileTitle = x.MobileTitleAz,
                    portfolioCat = x.PortfolioCategories?.Select(z => new
                    {
                        CategoryId = z.CategoryId,
                        CategoryName = z.Category.NameAz
                    }),
                    portfolioImg = x.Images?.Select(y => new
                    {
                        y.Id,
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
            var totalCount = await _unitOfWork.PortfolioRepository.GetTotalCountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            if (pageNumber > totalPages)
            {
                pageNumber = totalPages;
            }
            else if (pageNumber < 1)
            {
                pageNumber = 1;
            }
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
                    x.Link,
                    x.Sound,
                    x.Slug,
                    x.IsMain,
                    x.MetaKeyword,
                    x.MetaTitle,
                    x.OgTitle,
                    x.MetaDescription,
                    x.OgDescription,
                    x.MobileTitle,
                    portfolioCat = x.PortfolioCategories?.Select(x => new
                    {
                        CategoryId = x.CategoryId,
                        CategoryName = x.Category.Name
                    }),
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
                    x.Link,
                    x.Sound,
                    Slug = x.Slug,
                    IsMain = x.IsMain,
                    MetaKeyword = x.MetaKeywordAz,
                    MetaTitle = x.MetaTitleAz,
                    OgTitle = x.OgTitleAz,
                    MetaDescription = x.MetaDescriptionAz,
                    OgDescription = x.OgDescriptionAz,
                    MobileTitle = x.MobileTitleAz,
                    portfolioCat = x.PortfolioCategories?.Select(x => new
                    {
                        CategoryId = x.CategoryId,
                        CategoryName = x.Category.NameAz
                    }),
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