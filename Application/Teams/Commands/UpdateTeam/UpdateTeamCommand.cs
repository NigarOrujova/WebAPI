using Application.Abstracts.Common;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Commands.UpdateTeam;

public record UpdateTeamCommand : IRequest<Team>
{
    public int Id { get; init; }
    public string FulllName { get; init; }
    public string? Job { get; init; }
    public string? ImageAlt { get; init; }
}
public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, Team>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTeamCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Team> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        Team entity = await _unitOfWork.TeamRepository.GetAsync(n => n.Id == request.Id)
             ?? throw new NullReferenceException();

        entity.FulllName = request.FulllName;
        entity.Job = request.Job;
        entity.ImageAlt = request.ImageAlt;

        await _unitOfWork.TeamRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}