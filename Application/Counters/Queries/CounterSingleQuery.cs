using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Counters.Queries;

public record CounterSingleQuery(int Id) : IRequest<Counter>;

internal class CounterSingleQueryHandler : IRequestHandler<CounterSingleQuery, Counter>
{
    private readonly IUnitOfWork _unitOfWork;

    public CounterSingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Counter> Handle(CounterSingleQuery request, CancellationToken cancellationToken)
    {
        Counter entity = await _unitOfWork.CounterRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();

        return entity;
    }
}
