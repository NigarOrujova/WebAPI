using Application.Abstracts.Common.Exceptions;
using Application.Abstracts.Common.Interfaces;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Application.Blogs.Commands.CreateBlog;

public record CreateBlogCommand:IRequest<int>
{
    public string Title { get; init; }
    public string TitleAz { get; init; }
    public string Description { get; init; }
    public string DescriptionAz { get; init; }
    public IFormFile Image { get; init; }
    public string ImageAlt { get; init; }
    public string ImageAltAz { get; init; }
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
    public DateTime? PublishDate { get; init; }
    public virtual List<int>? TagIds { get; init; } = new List<int>();
}
public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostEnvironment _env;

    public CreateBlogCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }

    public async Task<int> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.BlogRepository.IsExistAsync(x => x.Title == request.Title)) 
            throw new FileException("Blog with the same title already exists.");

        var entity = new Blog();

        entity.Title = request.Title;
        entity.TitleAz = request.TitleAz;
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
        entity.ImageAlt = request.ImageAlt;
        entity.ImageAltAz = request.ImageAltAz;
        entity.Slug = request.Title.ToSlug();
        if (request.TagIds != null)
        {
            entity.TagCloud = new List<BlogTagCloud>();
            foreach (var id in request.TagIds)
            {
                BlogTagCloud BlogCategory = new BlogTagCloud()
                {
                    TagId = id,
                    Blog = entity
                };
                entity.TagCloud.Add(BlogCategory);
            }
        }
        if (request.Image != null)
        {
            if (!request.Image.CheckFileSize(1000))
                throw new FileException("File max size 1 mb");
            if (!request.Image.CheckFileType("image/"))
                throw new FileException("File type must be image");
            string newImageName = request.Image.GetRandomImagePath("blog");
            await _env.SaveAsync(request.Image, newImageName, cancellationToken);
            entity.ImagePath = newImageName;
        }
        await _unitOfWork.BlogRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}