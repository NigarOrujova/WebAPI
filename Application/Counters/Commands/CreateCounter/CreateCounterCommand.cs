using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Counters.Commands.CreateCounter;

public record CreateCounterCommand : IRequest<int>
{
    public int Year { get; init; }
    public string? YearText { get; init; }
    public string? YearTextAz { get; init; }
}
public class CreateCounterCommandHandler : IRequestHandler<CreateCounterCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCounterCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateCounterCommand request, CancellationToken cancellationToken)
    {
        var entity = new Counter();

        entity.Year = request.Year;
        entity.YearText = request.YearText;
        entity.YearTextAz = request.YearTextAz;

        await _unitOfWork.CounterRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}