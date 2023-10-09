using Application.Abstracts.Common;
using Domain.Entities;
using MediatR;

namespace Application.Medias.Commands.DeleteMedia;

public record DeleteMediaCommand(int Id) : IRequest<bool>;

internal class DeleteMediaCommandHandler : IRequestHandler<DeleteMediaCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMediaCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteMediaCommand request, CancellationToken cancellationToken)
    {
        Media Media = await _unitOfWork.MediaRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();

        await _unitOfWork.MediaRepository.DeleteAsync(Media);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
