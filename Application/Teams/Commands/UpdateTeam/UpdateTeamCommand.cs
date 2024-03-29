﻿using Application.Abstracts.Common.Exceptions;
using Application.Abstracts.Common.Interfaces;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Application.Teams.Commands.UpdateTeam;

public record UpdateTeamCommand(int Id,Team Team) : IRequest<Team>;
public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, Team>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostEnvironment _env;

    public UpdateTeamCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }

    public async Task<Team> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        Team entity = await _unitOfWork.TeamRepository.GetAsync(n => n.Id == request.Id)
             ?? throw new NullReferenceException();
        if (request.Team.Image == null)
        {
            request.Team.Image = entity.Image;
            goto save;
        }

        if (!request.Team.Image.CheckFileSize(1000))
            throw new FileException("File max size 1 mb");
        if (!request.Team.Image.CheckFileType("image/"))
            throw new FileException("File type must be image");
        string newImageName = request.Team.Image.GetRandomImagePath("Teams");

        if (entity.ImagePath != null)
        {
            _env.ArchiveImage(entity.ImagePath);
        }
        await _env.SaveAsync(request.Team.Image, newImageName, cancellationToken);

        entity.ImagePath = newImageName;

    save:
        if (request.Team.Image2 == null)
        {
            request.Team.Image2 = entity.Image2;
            goto saveimg;
        }

        if (!request.Team.Image2.CheckFileSize(1000))
            throw new FileException("File max size 1 mb");
        if (!request.Team.Image2.CheckFileType("image/"))
            throw new FileException("File type must be image");
        string newImageName2 = request.Team.Image2.GetRandomImagePath("Teams");

        if (entity.ImagePath2 != null)
        {
            _env.ArchiveImage(entity.ImagePath2);
        }
        await _env.SaveAsync(request.Team.Image2, newImageName2, cancellationToken);

        entity.ImagePath2 = newImageName2;

    saveimg:
        entity.FulllName = request.Team.FulllName;
        entity.FulllNameAz = request.Team.FulllNameAz;
        entity.Job = request.Team.Job;
        entity.JobAz = request.Team.JobAz;
        entity.ImageAlt = request.Team.ImageAlt;
        entity.ImageAltAz = request.Team.ImageAltAz;

        await _unitOfWork.TeamRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}