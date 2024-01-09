using Application.Abstracts.Common.Exceptions;
using Application.Abstracts.Common.Interfaces;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Application.Awards.Commands.UpdateAward;

public record UpdateAwardCommand(int Id, Award Award) : IRequest<Award>;
public class UpdateAwardCommandHandler : IRequestHandler<UpdateAwardCommand, Award>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostEnvironment _env;

    public UpdateAwardCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }

    public async Task<Award> Handle(UpdateAwardCommand request, CancellationToken cancellationToken)
    {
        Award entity = await _unitOfWork.AwardRepository.GetAsync(n => n.Id == request.Id);

        if (entity == null)
            throw new FileException("Award Not fount");

        if (request.Award.Image == null)
        {
            request.Award.Image = entity.Image;
            goto save;
        }

        if (!request.Award.Image.CheckFileSize(1000))
            throw new FileException("File max size 1 mb");
        if (!request.Award.Image.CheckFileType("image/"))
            throw new FileException("File type must be image");
        string newImageName = request.Award.Image.GetRandomImagePath("Award");

        _env.ArchiveImage(entity.ImagePath);
        await _env.SaveAsync(request.Award.Image, newImageName, cancellationToken);

        entity.ImagePath = newImageName;

    save:
        entity.ImageAlt = request.Award.ImageAlt;
        entity.AwardName=request.Award.AwardName;
        entity.Year= request.Award.Year;
        entity.Project= request.Award.Project;
        entity.Contest= request.Award.Contest;

        await _unitOfWork.AwardRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}