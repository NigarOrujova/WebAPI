using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.OurValues.Commands.UpdateOurValue;

public record UpdateOurValueCommand(int Id,OurValue OurValue) : IRequest<OurValue>;
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

        entity.Title = request.OurValue.Title;
        entity.TitleAz = request.OurValue.TitleAz;
        entity.Description = request.OurValue.Description;
        entity.DescriptionAz = request.OurValue.DescriptionAz;

        await _unitOfWork.OurValueRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}