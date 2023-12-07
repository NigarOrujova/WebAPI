using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Queries;

public record TeamLanguageAllQuery : IRequest<object>;
public class TeamLanguageAllQueryHandler : IRequestHandler<TeamLanguageAllQuery,object>
{
    private readonly IUnitOfWork _unitOfWork;

    public TeamLanguageAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(TeamLanguageAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Team> Teams = await _unitOfWork.TeamRepository.GetAllAsync()
            ?? throw new NullReferenceException();

        var data = new
        {
            team_en = Teams.Select(t => new
            {
                t.Id,
                t.FulllName,
                t.Rank,
                t.Job,
                t.ImagePath,
                t.ImagePath2,
                t.ImageAlt
            }),
            team_az=Teams.Select(t => new
            {
                t.Id,
                FulllName = t.FulllNameAz,
                t.Rank,
                Job = t.JobAz,
                ImagePath = t.ImagePath,
                ImagePath2 = t.ImagePath2,
                ImageAlt = t.ImageAltAz
            })
        };

        return data;
    }
}