using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Headers.Commands.UpdateHeader;

public record UpdateHeaderCommand(int Id, Header Header) : IRequest<Header>;
public class UpdateHeaderCommandHandler : IRequestHandler<UpdateHeaderCommand, Header>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateHeaderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Header> Handle(UpdateHeaderCommand request, CancellationToken cancellationToken)
    {
        Header entity = await _unitOfWork.HeaderRepository.GetAsync(n => n.Id == request.Id);

        entity.Title = request.Header.Title;
        entity.TitleAz = request.Header.TitleAz;

        await _unitOfWork.HeaderRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}