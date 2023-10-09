using Application.Abstracts.Common;
using Domain.Entities;
using MediatR;

namespace Application.Medias.Queries;

public record MediaAllQuery:IRequest<IEnumerable<Media>>;
public class MediaAllQueryHandler : IRequestHandler<MediaAllQuery, IEnumerable<Media>>
{
    private readonly IUnitOfWork _unitOfWork;

    public MediaAllQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Media>> Handle(MediaAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Media> Medias = await _unitOfWork.MediaRepository.GetAllAsync()
            ?? throw new NullReferenceException();

        return Medias;
    }
}
