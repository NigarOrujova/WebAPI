using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.OurValues.Commands.UpdateOurValue;

public record UpdateOurValueCommand : IRequest<OurValue>
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
}
public class UpdateOurValueCommandHandler : IRequestHandler<UpdateOurValueCommand, OurValue>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOurValueCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OurValue> Handle(UpdateOurValueCommand request, CancellationToken cancellationToken)
    {
        OurValue entity = await _unitOfWork.OurValueRepository.GetAsync(n => n.Id == request.Id)
             ?? throw new NullReferenceException();

        entity.Title = request.Title;
        entity.Description = request.Description;

        await _unitOfWork.OurValueRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}