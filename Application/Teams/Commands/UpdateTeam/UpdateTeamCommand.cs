﻿using Application.Abstracts.Common;
using Application.Extensions;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Application.Teams.Commands.UpdateTeam;

public record UpdateTeamCommand : IRequest<Team>
{
    public int Id { get; init; }
    public string FulllName { get; init; }
    public string? Job { get; init; }
    public IFormFile? Image { get; set; }
    public IFormFile? Image2 { get; set; }
    public string? ImageAlt { get; init; }
}
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
        if (request.Image == null)
        {
            request.Image = entity.Image;
            goto save;
        }

        if (!request.Image.CheckFileSize(1000))
            throw new FileException("File max size 1 mb");
        if (!request.Image.CheckFileType("image/"))
            throw new FileException("File type must be image");
        string newImageName = request.Image.GetRandomImagePath("customer");

        _env.ArchiveImage(entity.ImagePath);
        await _env.SaveAsync(request.Image, newImageName, cancellationToken);

        entity.ImagePath = newImageName;

    save:
        if (request.Image2 == null)
        {
            request.Image2 = entity.Image2;
            goto saveimg;
        }

        if (!request.Image2.CheckFileSize(1000))
            throw new FileException("File max size 1 mb");
        if (!request.Image2.CheckFileType("image/"))
            throw new FileException("File type must be image");
        string newImageName2 = request.Image2.GetRandomImagePath("customer");

        _env.ArchiveImage(entity.ImagePath2);
        await _env.SaveAsync(request.Image2, newImageName2, cancellationToken);

        entity.ImagePath2 = newImageName2;

    saveimg:
        entity.FulllName = request.FulllName;
        entity.Job = request.Job;
        entity.ImageAlt = request.ImageAlt;

        await _unitOfWork.TeamRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}