using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Headers.Commands.DeleteHeader;

public record DeleteHeaderCommand(int Id) : IRequest<bool>;

internal class DeteleHeaderCommandHandler : IRequestHandler<DeleteHeaderCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeteleHeaderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteHeaderCommand request, CancellationToken cancellationToken)
    {
        Header Header = await _unitOfWork.HeaderRepository.GetAsync(n => n.Id == request.Id)
        ?? throw new NullReferenceException();

        await _unitOfWork.HeaderRepository.DeleteAsync(Header);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}