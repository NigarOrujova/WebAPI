using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.OurValues.Queries;

public record OurValueSingleQuery(int Id) : IRequest<OurValue>;

internal class OurValueSingleQueryHandler : IRequestHandler<OurValueSingleQuery, OurValue>
{
    private readonly IUnitOfWork _unitOfWork;

    public OurValueSingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OurValue> Handle(OurValueSingleQuery request, CancellationToken cancellationToken)
    {
        OurValue entity = await _unitOfWork.OurValueRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();

        return entity;
    }
}