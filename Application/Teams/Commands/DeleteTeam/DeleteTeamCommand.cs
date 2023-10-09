using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Commands.DeleteTeam;

public record DeleteTeamCommand(int Id) : IRequest<bool>;

internal class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTeamCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        Team Team = await _unitOfWork.TeamRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();

        await _unitOfWork.TeamRepository.DeleteAsync(Team);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
