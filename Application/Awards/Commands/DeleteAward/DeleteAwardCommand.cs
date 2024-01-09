using Application.Abstracts.Common.Interfaces;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Application.Awards.Commands.DeleteAward;

public record DeteleAwardCommand(int Id) : IRequest<bool>;

internal class DeteleAwardCommandHandler : IRequestHandler<DeteleAwardCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostEnvironment _env;

    public DeteleAwardCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }

    public async Task<bool> Handle(DeteleAwardCommand request, CancellationToken cancellationToken)
    {
        Award Award = await _unitOfWork.AwardRepository.GetAsync(n => n.Id == request.Id)
        ?? throw new NullReferenceException();

        if (Award.ImagePath != null)
        {
            _env.ArchiveImage(Award.ImagePath);
        }
        await _unitOfWork.AwardRepository.DeleteAsync(Award);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}