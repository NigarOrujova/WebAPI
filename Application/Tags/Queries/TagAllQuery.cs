using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Tags.Queries;

public record TagAllQuery : IRequest<IEnumerable<Tag>>;
public class TagAllQueryHandler : IRequestHandler<TagAllQuery, IEnumerable<Tag>>
{
    private readonly IUnitOfWork _unitOfWork;

    public TagAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Tag>> Handle(TagAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Tag> Tags = await _unitOfWork.TagRepository.GetAllAsync(
        includes: new Expression<Func<Tag, object>>[]
        {
            x => x.TagCloud
        })
            ?? throw new NullReferenceException();

        IEnumerable<Blog> Blogs = await _unitOfWork.BlogRepository.GetAllAsync(
        includes: x => x.TagCloud)
           ?? throw new NullReferenceException();

        return Tags;
    }
}