using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Counters.Queries;

public record CounterLanguageQuery(int Id) : IRequest<object>;
internal class CounterLanguageQueryHandler : IRequestHandler<CounterLanguageQuery, object>
{
    private readonly IUnitOfWork _unitOfWork;

    public CounterLanguageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(CounterLanguageQuery request, CancellationToken cancellationToken)
    {
        Counter entity = await _unitOfWork.CounterRepository.GetAsync(n => n.Id == request.Id) 
            ?? throw new NullReferenceException();

        var data = new
        {
            Counter_en = new
            {
                entity.Year,
                YearText = entity.YearText ?? ""
            },
            Counter_az = new
            {
                entity.Year,
                YearText = entity.YearTextAz ?? "",
            }
        };
        return data;
    }
}