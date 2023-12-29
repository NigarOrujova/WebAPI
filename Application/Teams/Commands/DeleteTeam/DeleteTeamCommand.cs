using Application.Abstracts.Common.Interfaces;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Application.Teams.Commands.DeleteTeam;

public record DeleteTeamCommand(int Id) : IRequest<bool>;

internal class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostEnvironment _env;

    public DeleteTeamCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }

    public async Task<bool> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        Team Team = await _unitOfWork.TeamRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();

        if(Team.ImagePath != null)
        {
            _env.ArchiveImage(Team.ImagePath);
        }
        if (Team.ImagePath2 != null)
        {
            _env.ArchiveImage(Team.ImagePath2);
        }
        await _unitOfWork.TeamRepository.DeleteAsync(Team);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
