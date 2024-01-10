using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Awards.Queries;

public record AwardLanguageQuery(int Id) : IRequest<object>;
internal class AwardLanguageQueryHandler : IRequestHandler<AwardLanguageQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public AwardLanguageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(AwardLanguageQuery request, CancellationToken cancellationToken)
    {
        Award entity = await _unitOfWork.AwardRepository.GetAsync(
            n => n.Id == request.Id) ?? throw new NullReferenceException();

        var data = new
        {
            Award_en = new
            {
                entity.Year,
                AwardName = entity.AwardName ?? "",
                Contest = entity.Contest ?? "",
                Project = entity.Project ?? "",
                img=entity.ImagePath,
                imgAlt=entity.ImageAlt
            },
            Award_az = new
            {
                entity.Year,
                AwardName = entity.AwardNameAz ?? "",
                Contest = entity.ContestAz ?? "",
                Project = entity.ProjectAz ?? "",
                img = entity.ImagePath,
                imgAlt = entity.ImageAlt
            }
        };
        return data;
    }
}