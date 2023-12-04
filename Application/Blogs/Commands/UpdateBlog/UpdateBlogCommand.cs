using Application.Abstracts.Common.Exceptions;
using Application.Abstracts.Common.Interfaces;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Application.Blogs.Commands.UpdateBlog;


public record UpdateBlogCommand(int Id, Blog Blog) : IRequest<Blog>;
public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, Blog>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostEnvironment _env;

    public UpdateBlogCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }

    public async Task<Blog> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.BlogRepository.IsExistAsync(x => x.Title == request.Blog.Title && x.Id != request.Id))
            throw new FileException("Blog with the same title already exists.");

        Blog entity = await _unitOfWork.BlogRepository.GetAsync(n => n.Id == request.Id,includes:x=>x.TagCloud);

        if (entity == null)
            throw new FileException("Blog Not fount");

        if (request.Blog.Image == null)
        {
            request.Blog.Image = entity.Image;
            goto save;
        }

        if (!request.Blog.Image.CheckFileSize(1000))
            throw new FileException("File max size 1 mb");
        if (!request.Blog.Image.CheckFileType("image/"))
            throw new FileException("File type must be image");
        string newImageName = request.Blog.Image.GetRandomImagePath("Blog");

        _env.ArchiveImage(entity.ImagePath);
        await _env.SaveAsync(request.Blog.Image, newImageName, cancellationToken);

        entity.ImagePath = newImageName;

    save:
        entity.ImageAlt = request.Blog.ImageAlt;
        entity.ImageAltAz = request.Blog.ImageAltAz;
        entity.Title = request.Blog.Title;
        entity.TitleAz = request.Blog.TitleAz;
        entity.Description = request.Blog.Description;
        entity.DescriptionAz = request.Blog.DescriptionAz;
        entity.MetaKeyword = request.Blog.MetaKeyword;
        entity.MetaKeywordAz = request.Blog.MetaKeywordAz;
        entity.MetaTitle = request.Blog.MetaTitle;
        entity.MetaTitleAz = request.Blog.MetaTitleAz;
        entity.OgTitle = request.Blog.OgTitle;
        entity.OgTitleAz = request.Blog.OgTitleAz;
        entity.MetaDescription = request.Blog.MetaDescription;
        entity.MetaDescriptionAz = request.Blog.MetaDescriptionAz;
        entity.OgDescription = request.Blog.OgDescription;
        entity.OgDescriptionAz = request.Blog.OgDescriptionAz;
        entity.MobileTitle = request.Blog.MobileTitle;
        entity.MobileTitleAz = request.Blog.MobileTitleAz;
        entity.Slug = entity.Title.ToSlug();

        if (request.Blog.TagIds != null)
        {
            if (entity.TagCloud == null)
            {
                entity.TagCloud = new List<BlogTagCloud>();
            }

            entity.TagCloud?.RemoveAll(x => !request.Blog.TagIds.Contains(x.TagId));
            foreach (var categoryId in request.Blog.TagIds.Where(x => !entity.TagCloud.Any(rc => rc.TagId == x)))
            {
                BlogTagCloud BlogCategory = new BlogTagCloud
                {
                    BlogId = request.Id,
                    TagId = categoryId
                };
                entity.TagCloud.Add(BlogCategory);
            }
        }
        await _unitOfWork.BlogRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}