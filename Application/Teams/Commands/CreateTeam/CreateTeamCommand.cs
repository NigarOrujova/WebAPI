using Application.Abstracts.Common;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Commands.CreateTeam;

public record CreateTeamCommand : IRequest<int>
{
    public string FulllName { get; init; } = null!;
    public string? Job { get; init; }
    public string? ImageAlt { get; init; }
}
public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTeamCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var entity = new Team();

        entity.FulllName = request.FulllName;
        entity.Job = request.Job;
        entity.ImageAlt = request.ImageAlt;

        await _unitOfWork.TeamRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}