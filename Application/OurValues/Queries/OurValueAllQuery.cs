using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.OurValues.Queries;

public record OurValueAllQuery:IRequest<IEnumerable<OurValue>>;
public class OurValueAllQueryHandler : IRequestHandler<OurValueAllQuery, IEnumerable<OurValue>>
{
    private readonly IUnitOfWork _unitOfWork;

    public OurValueAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<OurValue>> Handle(OurValueAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<OurValue> ourValues = await _unitOfWork.OurValueRepository.GetAllAsync()
            ?? throw new NullReferenceException();

        return ourValues;
    }
}
