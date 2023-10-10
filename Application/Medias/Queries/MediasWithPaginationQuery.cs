using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Medias.Queries;

public class MediasWithPaginationQuery:IRequest<List<Media>>
{
    public int Page { get; set; }
    public int Size { get; set; }
}
public class MediasWithPaginationQueryHandler:IRequestHandler<MediasWithPaginationQuery, List<Media>>
{
    private readonly IUnitOfWork _unitOfWork;

    public MediasWithPaginationQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Media>> Handle(MediasWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return (List<Media>)await _unitOfWork.MediaRepository.GetPaginatedAsync(request.Page,request.Size);
    }
}
