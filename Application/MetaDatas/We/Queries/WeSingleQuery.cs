using Application.Abstracts.Common.Interfaces;
using MediatR;

namespace Application.MetaDatas.We.Queries;

public record WeSingleQuery : IRequest<Domain.Entities.We>;
internal class WeSingleQueryHandler : IRequestHandler<WeSingleQuery, Domain.Entities.We>
{
    private readonly IUnitOfWork _unitOfWork;

    public WeSingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Domain.Entities.We> Handle(WeSingleQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.WeRepository.GetAsync()
            ?? throw new NullReferenceException();

        return entity;
    }
}