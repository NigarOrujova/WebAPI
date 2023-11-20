using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Blogs.Queries;

public class BlogsWithPaginationQuery : IRequest<List<Blog>>
{
    public int Page { get; set; }
    public int Size { get; set; }
}
public class BlogsWithPaginationQueryHandler : IRequestHandler<BlogsWithPaginationQuery, List<Blog>>
{
    private readonly IUnitOfWork _unitOfWork;

    public BlogsWithPaginationQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Blog>> Handle(BlogsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return (List<Blog>)await _unitOfWork.BlogRepository.GetPaginatedAsync(request.Page, request.Size);
    }
}
