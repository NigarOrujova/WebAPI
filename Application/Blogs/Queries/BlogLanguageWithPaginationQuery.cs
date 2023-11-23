using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Blogs.Queries;

public class BlogLanguageWithPaginationQuery : IRequest<object>
{
    public int Page { get; set; }
    public int Size { get; set; }
    public int? TagId { get; set; }
}
public class BlogLanguageWithPaginationQueryHandler : IRequestHandler<BlogLanguageWithPaginationQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public BlogLanguageWithPaginationQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(BlogLanguageWithPaginationQuery request, CancellationToken cancellationToken)
    {
        int pageSize = request.Size;
        int pageNumber = request.Page;

        var totalCount = await _unitOfWork.BlogRepository.GetTotalCountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        IEnumerable<Blog> Blogs = await _unitOfWork.BlogRepository.GetAllAsync(
        includes: x => x.TagCloud)
            ?? throw new NullReferenceException();

        

        if (pageNumber > totalPages)
        {
            pageNumber = totalPages;
        }
        else if (pageNumber < 1)
        {
            pageNumber = 1;
        }

        if (request.TagId > 0)
        {
            var filteredBlogs = Blogs
               .Where(x => x.TagCloud.Any(y => y.TagId == request.TagId))
               .OrderByDescending(x => x.Id)
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToList();

            var model = new
            {
                project_en = filteredBlogs
                .Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.Description,
                    x.ImagePath,
                    x.Slug,
                    x.MetaKeyword,
                    x.MetaTitle,
                    x.OgTitle,
                    x.MetaDescription,
                    x.OgDescription,
                    x.MobileTitle,
                    PublishDate = x.PublishDate?.ToString("MMMM dd, yyyy") ?? "",
                    BlogCat = x.TagCloud?.Where(y => y != null && y.TagId != 0).Select(x => new
                    {
                        Tags = new
                        {
                            
                        }
                    })
                }).ToList(),
                project_az = filteredBlogs
                .Select(x => new
                {
                    x.Id,
                    Title = x.TitleAz,
                    Description = x.DescriptionAz,
                    Slug = x.Slug,
                    x.ImagePath,
                    MetaKeyword = x.MetaKeywordAz,
                    MetaTitle = x.MetaTitleAz,
                    OgTitle = x.OgTitleAz,
                    MetaDescription = x.MetaDescriptionAz,
                    OgDescription = x.OgDescriptionAz,
                    MobileTitle = x.MobileTitleAz,
                    PublishDate = x.PublishDate?.ToString("MMMM dd, yyyy") ?? "",
                    BlogCat = x.TagCloud?.Where(y => y != null && y.TagId != 0).Select(x => x.Tag)
                }).ToList(),
                totalPages = totalPages
            };

            return model;
        }
        else
        {
            var model = new
            {
                project_en = Blogs
                .OrderByDescending(x => x.Id)
                .Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.Description,
                    x.ImagePath,
                    x.Slug,
                    x.MetaKeyword,
                    x.MetaTitle,
                    x.OgTitle,
                    x.MetaDescription,
                    x.OgDescription,
                    x.MobileTitle,
                    PublishDate = x.PublishDate?.ToString("MMMM dd, yyyy") ?? "",
                    BlogCat = x.TagCloud?.Where(y => y != null && y.TagId != 0).Select(x => x.TagId)
                }).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(),
                project_az = Blogs
                .OrderByDescending(x => x.Id)
                .Select(x => new
                {
                    x.Id,
                    Title = x.TitleAz,
                    Description = x.DescriptionAz,
                    x.ImagePath,
                    Slug = x.Slug,
                    MetaKeyword = x.MetaKeywordAz,
                    MetaTitle = x.MetaTitleAz,
                    OgTitle = x.OgTitleAz,
                    MetaDescription = x.MetaDescriptionAz,
                    OgDescription = x.OgDescriptionAz,
                    MobileTitle = x.MobileTitleAz,
                    PublishDate = x.PublishDate?.ToString("MMMM dd, yyyy") ?? "",
                    BlogCat = x.TagCloud?.Where(y => y != null && y.TagId != 0).Select(x => x.TagId)
                }).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(),
                totalPages = totalPages
            };

            return model;
        }
    }
}