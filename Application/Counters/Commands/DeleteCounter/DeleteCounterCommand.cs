using Application.Abstracts.Common.Interfaces;
using MediatR;
using Domain.Entities;
using Microsoft.Extensions.Hosting;

namespace Application.Counters.Commands.DeleteCounter;

public record DeteleCounterCommand(int Id) : IRequest<bool>;

internal class DeteleCounterCommandHandler : IRequestHandler<DeteleCounterCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostEnvironment _env;

    public DeteleCounterCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }

    public async Task<bool> Handle(DeteleCounterCommand request, CancellationToken cancellationToken)
    {
        Counter Counter = await _unitOfWork.CounterRepository.GetAsync(n => n.Id == request.Id)
        ?? throw new NullReferenceException();

        await _unitOfWork.CounterRepository.DeleteAsync(Counter);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}