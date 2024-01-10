using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Counters.Queries;

public record CounterAllQuery : IRequest<IEnumerable<Counter>>;
public class CounterAllQueryHandler : IRequestHandler<CounterAllQuery, IEnumerable<Counter>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CounterAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Counter>> Handle(CounterAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Counter> Counters = await _unitOfWork.CounterRepository.GetAllAsync()
            ?? throw new NullReferenceException();

        return Counters;
    }
}