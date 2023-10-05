using Application.Abstracts.Common;
using Domain.Entities;
using MediatR;

namespace Application.OurValues.Commands.CreateOurValue;

public record CreateOurValueCommand : IRequest<int>
{
    public string Title { get; init; } = null!;
    public string Description { get; init; }=null!;
}
public class CreateOurValueCommandHandler : IRequestHandler<CreateOurValueCommand, int>
{
    private readonly IYelloadDbContext _context;

    public CreateOurValueCommandHandler(IYelloadDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateOurValueCommand request, CancellationToken cancellationToken)
    {
        var entity = new OurValue();

        entity.Title = request.Title;

        _context.OurValues.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}