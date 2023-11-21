using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.MetaDatas.Blogs.Queries;

public record BlogMetaSingleQuery : IRequest<BlogMeta>;
internal class BlogMetaSingleQueryHandler : IRequestHandler<BlogMetaSingleQuery, BlogMeta>
{
    private readonly IUnitOfWork _unitOfWork;

    public BlogMetaSingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BlogMeta> Handle(BlogMetaSingleQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.BlogMetaRepository.GetAsync()
            ?? throw new NullReferenceException();

        return entity;
    }
}