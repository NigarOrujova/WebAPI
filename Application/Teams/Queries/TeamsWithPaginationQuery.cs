using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Queries;

public class TeamsWithPaginationQuery:IRequest<List<Team>>
{
    public int Page { get; set; }
    public int Size { get; set; }
}
public class TeamsWithPaginationQueryHandler:IRequestHandler<TeamsWithPaginationQuery, List<Team>>
{
    private readonly IUnitOfWork _unitOfWork;

    public TeamsWithPaginationQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Team>> Handle(TeamsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return (List<Team>)await _unitOfWork.TeamRepository.GetPaginatedAsync(request.Page,request.Size);
    }
}
