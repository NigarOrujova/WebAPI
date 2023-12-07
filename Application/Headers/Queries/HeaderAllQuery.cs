using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Headers.Queries;

public record HeaderAllQuery : IRequest<IEnumerable<Header>>;
public class HeaderAllQueryHandler : IRequestHandler<HeaderAllQuery, IEnumerable<Header>>
{
    private readonly IUnitOfWork _unitOfWork;

    public HeaderAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Header>> Handle(HeaderAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Header> Headers = await _unitOfWork.HeaderRepository.GetAllAsync()
            ?? throw new NullReferenceException();

        return Headers;
    }
}