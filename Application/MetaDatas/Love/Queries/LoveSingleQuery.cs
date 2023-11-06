using Application.Abstracts.Common.Interfaces;
using MediatR;

namespace Application.MetaDatas.Love.Queries;

public record LoveSingleQuery : IRequest<Domain.Entities.Love>;
internal class LoveSingleQueryHandler : IRequestHandler<LoveSingleQuery, Domain.Entities.Love>
{
    private readonly IUnitOfWork _unitOfWork;

    public LoveSingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Domain.Entities.Love> Handle(LoveSingleQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.LoveRepository.GetAsync()
            ?? throw new NullReferenceException();

        return entity;
    }
}