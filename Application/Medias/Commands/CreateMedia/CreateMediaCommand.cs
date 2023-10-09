using Application.Abstracts.Common;
using Domain.Entities;
using MediatR;

namespace Application.Medias.Commands.CreateMedia;

public record CreateMediaCommand : IRequest<int>
{
    public string Title { get; init; } = null!;
    public string URL { get; init; }=null!;
}
public class CreateMediaCommandHandler : IRequestHandler<CreateMediaCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMediaCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateMediaCommand request, CancellationToken cancellationToken)
    {
        var entity = new Media();

        entity.Title = request.Title;
        entity.URL = request.URL;

        await _unitOfWork.MediaRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}