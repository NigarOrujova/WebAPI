using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.MetaDatas.Wes.Queries;

public record WeSingleQuery : IRequest<We>;
internal class WeSingleQueryHandler : IRequestHandler<WeSingleQuery, We>
{
    private readonly IUnitOfWork _unitOfWork;

    public WeSingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<We> Handle(WeSingleQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.WeRepository.GetAsync()
            ?? throw new NullReferenceException();

        return entity;
    }
}