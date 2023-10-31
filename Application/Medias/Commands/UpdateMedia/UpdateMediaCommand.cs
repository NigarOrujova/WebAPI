using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Medias.Commands.UpdateMedia;

public record UpdateMediaCommand(int Id,Media request) : IRequest<Media>;
public class UpdateMediaCommandHandler : IRequestHandler<UpdateMediaCommand, Media>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMediaCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Media> Handle(UpdateMediaCommand request, CancellationToken cancellationToken)
    {
        Media entity = await _unitOfWork.MediaRepository.GetAsync(n => n.Id == request.Id)
             ?? throw new NullReferenceException();

        entity.Title = request.request.Title;
        entity.URL = request.request.URL;
        entity.FooterId = 1;

        await _unitOfWork.MediaRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}