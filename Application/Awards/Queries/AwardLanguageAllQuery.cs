using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Awards.Queries;

public record AwardLanguageAllQuery : IRequest<object>;
public class AwardLanguageAllQueryHandler : IRequestHandler<AwardLanguageAllQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public AwardLanguageAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(AwardLanguageAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Award> Awards = await _unitOfWork.AwardRepository.GetAllAsync()
            ?? throw new NullReferenceException();

        var data = new
        {
            Award_en = Awards.Select(p => new
            {
                p.Id,
                p.Year,
                AwardName = p.AwardName ?? "",
                Contest = p.Contest ?? "",
                Project = p.Project ?? "",
                img = p.ImagePath,
                imgAlt = p.ImageAlt
            }),
            Award_az = Awards.Select(p => new
            {
                p.Id,
                p.Year,
                AwardName = p.AwardNameAz ?? "",
                Contest = p.ContestAz ?? "",
                Project = p.ProjectAz ?? "",
                img = p.ImagePath,
                imgAlt = p.ImageAlt
            })
        };

        return data;
    }
}