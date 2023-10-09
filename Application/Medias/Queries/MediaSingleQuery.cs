using Application.Abstracts.Common;
using Domain.Entities;
using MediatR;

namespace Application.Medias.Queries;

public record MediaSingleQuery(int Id) : IRequest<Media>;

internal class MediaSingleQueryHandler : IRequestHandler<MediaSingleQuery, Media>
{
    private readonly IUnitOfWork _unitOfWork;

    public MediaSingleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Media> Handle(MediaSingleQuery request, CancellationToken cancellationToken)
    {
        Media entity = await _unitOfWork.MediaRepository.GetAsync(n => n.Id == request.Id)
            ?? throw new NullReferenceException();

        return entity;
    }
}