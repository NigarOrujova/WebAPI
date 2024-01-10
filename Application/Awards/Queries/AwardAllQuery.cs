using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Awards.Queries;

public record AwardAllQuery : IRequest<IEnumerable<Award>>;
public class AwardAllQueryHandler : IRequestHandler<AwardAllQuery, IEnumerable<Award>>
{
    private readonly IUnitOfWork _unitOfWork;

    public AwardAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Award>> Handle(AwardAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Award> Awards = await _unitOfWork.AwardRepository.GetAllAsync()
            ?? throw new NullReferenceException();

        return Awards;
    }
}