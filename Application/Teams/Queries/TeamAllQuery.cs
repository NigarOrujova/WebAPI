using Application.Abstracts.Common;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Queries;

public record TeamAllQuery:IRequest<IEnumerable<Team>>;
public class TeamAllQueryHandler : IRequestHandler<TeamAllQuery, IEnumerable<Team>>
{
    private readonly IUnitOfWork _unitOfWork;

    public TeamAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Team>> Handle(TeamAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Team> Teams = await _unitOfWork.TeamRepository.GetAllAsync()
            ?? throw new NullReferenceException();

        return Teams;
    }
}
