using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Queries;

public record TeamLanguageQuery(int Id):IRequest<object>;
internal class TeamLanguageQueryHandler : IRequestHandler<TeamLanguageQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public TeamLanguageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(TeamLanguageQuery request, CancellationToken cancellationToken)
    {
        Team entity = await _unitOfWork.TeamRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();
        var data = new
        {
            team_en = new
            {
                entity.FulllName,
                entity.Rank,
                entity.Job,
                entity.ImagePath,
                entity.ImagePath2,
                entity.ImageAlt
            },
            team_az = new
            {
                FulllName=entity.FulllNameAz,
                Rank=entity.Rank,
                Job=entity.JobAz,
                ImagePath = entity.ImagePath,
                ImagePath2 = entity.ImagePath2,
                ImageAlt = entity.ImageAltAz
            }
        };
        return data;
    }
}
