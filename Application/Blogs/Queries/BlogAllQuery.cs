using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Blogs.Queries;

public record BlogAllQuery : IRequest<IEnumerable<Blog>>;
public class BlogAllQueryHandler : IRequestHandler<BlogAllQuery, IEnumerable<Blog>>
{
    private readonly IUnitOfWork _unitOfWork;

    public BlogAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Blog>> Handle(BlogAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Blog> Blogs = await _unitOfWork.BlogRepository.GetAllAsync(
        includes: new Expression<Func<Blog, object>>[]
        {
            x => x.TagCloud
        })
            ?? throw new NullReferenceException();

        return Blogs;
    }
}