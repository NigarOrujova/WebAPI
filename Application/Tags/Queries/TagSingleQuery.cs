using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Tags.Queries;

public record TagSingleQuery(int Id) : IRequest<Tag>;

internal class TagSingleQueryHandler : IRequestHandler<TagSingleQuery, Tag>
{
    private readonly IUnitOfWork _unitOfWork;

    public TagSingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Tag> Handle(TagSingleQuery request, CancellationToken cancellationToken)
    {
        Tag entity = await _unitOfWork.TagRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();

        return entity;
    }
}
