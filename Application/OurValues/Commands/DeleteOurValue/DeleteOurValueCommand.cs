using Application.Abstracts.Common;
using Domain.Entities;
using MediatR;

namespace Application.OurValues.Commands.DeleteOurValue;

public record DeleteOurValueCommand(int Id) : IRequest<bool>;

internal class DeleteOurValueCommandHandler : IRequestHandler<DeleteOurValueCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOurValueCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteOurValueCommand request, CancellationToken cancellationToken)
    {
        OurValue ourValue = await _unitOfWork.OurValueRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();

        await _unitOfWork.OurValueRepository.DeleteAsync(ourValue);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
