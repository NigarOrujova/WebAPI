using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Headers.Queries;

public record HeaderSingleQuery(int Id) : IRequest<Header>;

internal class HeaderSingleQueryHandler : IRequestHandler<HeaderSingleQuery, Header>
{
    private readonly IUnitOfWork _unitOfWork;

    public HeaderSingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Header> Handle(HeaderSingleQuery request, CancellationToken cancellationToken)
    {
        Header entity = await _unitOfWork.HeaderRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();

        return entity;
    }
}
