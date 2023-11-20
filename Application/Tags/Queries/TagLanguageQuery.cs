using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Tags.Queries;

public record TagLanguageQuery(int Id) : IRequest<object>;
internal class TagLanguageQueryHandler : IRequestHandler<TagLanguageQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public TagLanguageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(TagLanguageQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Tag, object>>[] includes = new Expression<Func<Tag, object>>[]
        {
            x => x.TagCloud
        };

        Tag entity = await _unitOfWork.TagRepository.GetAsync(
            n => n.Id == request.Id,
            includes: includes
        ) ?? throw new NullReferenceException();

        var data = new
        {
            Tag_en = new
            {
                Name = entity.Name ?? "",
                BlogCat = entity.TagCloud?.Where(x => x != null && x.BlogId != 0).Select(x => x.BlogId)
            },
            Tag_az = new
            {
                Name = entity.NameAz,
                BlogCat = entity.TagCloud?.Where(x => x != null && x.BlogId != 0).Select(x => x.BlogId)
            }
        };
        return data;
    }
}