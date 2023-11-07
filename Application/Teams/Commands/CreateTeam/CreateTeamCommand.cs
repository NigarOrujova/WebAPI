using Application.Abstracts.Common.Interfaces;
using Application.Extensions;
using Domain.Entities;
using Application.Abstracts.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Application.Teams.Commands.CreateTeam;

public record CreateTeamCommand : IRequest<int>
{
    public string FulllName { get; init; } = null!;
    public string FulllNameAz { get; init; }
    public string? Job { get; init; }
    public string? JobAz { get; init; }
    public IFormFile? Image { get; set; }
    public IFormFile? Image2 { get; set; }
    public string? ImageAlt { get; init; }
    public string? ImageAltAz { get; init; }
}
public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostEnvironment _env;

    public CreateTeamCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }

    public async Task<int> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var entity = new Team();

        entity.FulllName = request.FulllName;
        entity.FulllNameAz = request.FulllNameAz;
        entity.Job = request.Job;
        entity.JobAz = request.JobAz;
        entity.ImageAlt = request.ImageAlt;
        entity.ImageAltAz = request.ImageAltAz;
        if (request.Image != null)
        {
            if (!request.Image.CheckFileSize(1000))
                throw new FileException("File max size 1 mb");
            if (!request.Image.CheckFileType("image/"))
                throw new FileException("File type must be image");
            string newImageName = request.Image.GetRandomImagePath("Teams");
            await _env.SaveAsync(request.Image, newImageName, cancellationToken);
            entity.ImagePath = newImageName;
        }
        if (request.Image2 != null)
        {
            if (!request.Image2.CheckFileSize(1000))
                throw new FileException("File max size 1 mb");
            if (!request.Image2.CheckFileType("image/"))
                throw new FileException("File type must be image");
            string newImageName2 = request.Image2.GetRandomImagePath("Teams");
            await _env.SaveAsync(request.Image2, newImageName2, cancellationToken);
            entity.ImagePath2 = newImageName2;
        }
        await _unitOfWork.TeamRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}