using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.OurValues.Commands.CreateOurValue;

public record CreateOurValueCommand : IRequest<int>
{
    public string Title { get; init; } = null!;
    public string TitleAz { get; init; }
    public string Description { get; init; }=null!;
    public string DescriptionAz { get; init; }
}
public class CreateOurValueCommandHandler : IRequestHandler<CreateOurValueCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateOurValueCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateOurValueCommand request, CancellationToken cancellationToken)
    {
        var entity = new OurValue();

        entity.Title = request.Title;
        entity.TitleAz = request.TitleAz;
        entity.Description = request.Description;
        entity.DescriptionAz = request.DescriptionAz;

        await _unitOfWork.OurValueRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}