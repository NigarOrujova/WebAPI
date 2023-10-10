using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.OurValues.Queries;

public class OurValuesWithPaginationQuery:IRequest<List<OurValue>>
{
    public int Page { get; set; }
    public int Size { get; set; }
}
public class OurValuesWithPaginationQueryHandler:IRequestHandler<OurValuesWithPaginationQuery, List<OurValue>>
{
    private readonly IUnitOfWork _unitOfWork;

    public OurValuesWithPaginationQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<OurValue>> Handle(OurValuesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return (List<OurValue>)await _unitOfWork.OurValueRepository.GetPaginatedAsync(request.Page,request.Size);
    }
}
