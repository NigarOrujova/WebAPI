using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Awards.Queries;

public record AwardSingleQuery(int Id) : IRequest<Award>;

internal class AwardSingleQueryHandler : IRequestHandler<AwardSingleQuery, Award>
{
    private readonly IUnitOfWork _unitOfWork;

    public AwardSingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Award> Handle(AwardSingleQuery request, CancellationToken cancellationToken)
    {
        Award entity = await _unitOfWork.AwardRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();

        return entity;
    }
}
