using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Tags.Commands.CreateTag;

public record CreateTagCommand : IRequest<int>
{
    public string? Name { get; init; }
    public string? NameAz { get; init; }
    public List<int>? BlogIds { get; set; } = new List<int>();
}
public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTagCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var entity = new Tag();

        entity.Name = request.Name;
        entity.NameAz = request.NameAz;
        if (request.BlogIds != null)
        {

            entity.TagCloud = new List<BlogTagCloud>();
            foreach (var id in request.BlogIds)
            {
                BlogTagCloud tagCloud = new BlogTagCloud()
                {
                    BlogId = id,
                    Tag = entity
                };
                entity.TagCloud.Add(tagCloud);
            }
        }
        await _unitOfWork.TagRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}