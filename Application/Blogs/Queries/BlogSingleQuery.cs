using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Blogs.Queries;

public record BlogSingleQuery : IRequest<object>
{
    public int Id { get; set; }
    public string? Slug { get; init; }

}

internal class BlogSingleQueryHandler : IRequestHandler<BlogSingleQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public BlogSingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(BlogSingleQuery request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.Slug))
        {
            if (await _unitOfWork.BlogRepository.IsExistAsync(x => x.Slug == request.Slug))
            {
                return await _unitOfWork.BlogRepository.GetBlogBySlugAsync(request.Slug);
            }
            else
            {
                throw new InvalidOperationException("slug is null");
            }
        }
        IEnumerable<Tag> Tags = await _unitOfWork.TagRepository.GetAllAsync(
        includes: x => x.TagCloud)
           ?? throw new NullReferenceException();

        Blog entity = await _unitOfWork.BlogRepository.GetAsync(n => n.Id == request.Id,
            includes: x => x.TagCloud)
            ?? throw new NullReferenceException();

        return entity;
    }
}