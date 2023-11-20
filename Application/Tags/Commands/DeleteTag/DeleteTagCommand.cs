using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Tags.Commands.DeleteTag;

public record DeteleTagCommand(int Id) : IRequest<bool>;

internal class DeteleTagCommandHandler : IRequestHandler<DeteleTagCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeteleTagCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeteleTagCommand request, CancellationToken cancellationToken)
    {
        Tag tag = await _unitOfWork.TagRepository.GetAsync(n => n.Id == request.Id, includes: new Expression<Func<Tag, object>>[]
        {
            x => x.TagCloud
        })
        ?? throw new NullReferenceException();

        await _unitOfWork.TagRepository.DeleteAsync(tag);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}