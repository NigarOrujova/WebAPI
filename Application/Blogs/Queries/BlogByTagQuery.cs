using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Blogs.Queries;

public record BlogByTagQuery:IRequest<List<Blog>>
{
    public int TagId { get; set; }
}
public class BlogPostsAllQueryHandler : IRequestHandler<BlogByTagQuery, List<Blog>>
{
    private readonly IUnitOfWork _unitOfWork;

    public BlogPostsAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Blog>> Handle(BlogByTagQuery request, CancellationToken cancellationToken)
    {
        //var data = await db.BlogPosts
        //    .Include(bp => bp.TagCloud.Where(tc => tc.TagId == request.TagId))
        // .Where(m => m.TagCloud.Any() && m.DeletedDate == null)

        //var data = await (from bp in db.Blogs
        //                  join tc in db.BlogTagCloud on bp.Id equals tc.BlogId
        //                  where tc.TagId == request.TagId
        //                  select bp)
        //            .Distinct()
        //            .ToListAsync(cancellationToken);

        return null;
    }
}