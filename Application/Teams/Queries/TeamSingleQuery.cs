using Application.Abstracts.Common;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Queries;

public record TeamSingleQuery(int Id) : IRequest<Team>;

internal class TeamSingleQueryHandler : IRequestHandler<TeamSingleQuery, Team>
{
    private readonly IUnitOfWork _unitOfWork;

    public TeamSingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Team> Handle(TeamSingleQuery request, CancellationToken cancellationToken)
    {
        Team entity = await _unitOfWork.TeamRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();

        return entity;
    }
}