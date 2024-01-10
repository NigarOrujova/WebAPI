using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Counters.Queries;

public record CounterLanguageAllQuery : IRequest<object>;
public class CounterLanguageAllQueryHandler : IRequestHandler<CounterLanguageAllQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public CounterLanguageAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(CounterLanguageAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Counter> Counters = await _unitOfWork.CounterRepository.GetAllAsync()
            ?? throw new NullReferenceException();

        var data = new
        {
            Counter_en = Counters.Select(p => new
            {
                p.Id,
                p.Year,
                YearText = p.YearText ?? ""
            }),
            Counter_az = Counters.Select(p => new
            {
                p.Id,
                p.Year,
                YearText = p.YearTextAz ?? ""
            })
        };

        return data;
    }
}