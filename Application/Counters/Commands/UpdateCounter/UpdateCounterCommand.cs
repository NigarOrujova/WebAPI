using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Counters.Commands.UpdateCounter;

public record UpdateCounterCommand(int Id, Counter Counter) : IRequest<Counter>;
public class UpdateCounterCommandHandler : IRequestHandler<UpdateCounterCommand, Counter>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCounterCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Counter> Handle(UpdateCounterCommand request, CancellationToken cancellationToken)
    {
        Counter entity = await _unitOfWork.CounterRepository.GetAsync(n => n.Id == request.Id)
             ?? throw new NullReferenceException();

        entity.YearText = request.Counter.YearText;
        entity.YearTextAz = request.Counter.YearTextAz;
        entity.Year = request.Counter.Year;

        await _unitOfWork.CounterRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}