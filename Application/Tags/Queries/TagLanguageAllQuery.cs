using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Tags.Queries;

public record TagLanguageAllQuery : IRequest<object>;
public class TagLanguageAllQueryHandler : IRequestHandler<TagLanguageAllQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public TagLanguageAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(TagLanguageAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Tag> Tags = await _unitOfWork.TagRepository.GetAllAsync(
        includes: new Expression<Func<Tag, object>>[]
        {
            x => x.TagCloud
        })
            ?? throw new NullReferenceException();

        var data = new
        {
            Tag_en = Tags.Select(p => new
            {
                p.Id,
                Name = p.Name ?? "",
                BlogCat = p.TagCloud?.Where(x => x != null && x.BlogId != 0).Select(x => x.BlogId)
            }),
            Tag_az = Tags.Select(p => new
            {
                p.Id,
                Name = p.NameAz,
                BlogCat = p.TagCloud?.Where(x => x != null && x.BlogId != 0).Select(x => x.BlogId)
            })
        };

        return data;
    }
}