using Application.Abstracts.Common.Interfaces;
using MediatR;

namespace Application.MetaDatas.Esc.Queries;
 
public record EscSingleQuery : IRequest<Domain.Entities.Esc>;

internal class EscSingleQueryHandler : IRequestHandler<EscSingleQuery, Domain.Entities.Esc>
{
    private readonly IUnitOfWork _unitOfWork;

    public EscSingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Domain.Entities.Esc> Handle(EscSingleQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.EscRepository.GetAsync()
            ?? throw new NullReferenceException();

        return entity;
    }
}
