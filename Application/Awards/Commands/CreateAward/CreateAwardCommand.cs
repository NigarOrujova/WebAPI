using Application.Abstracts.Common.Exceptions;
using Application.Abstracts.Common.Interfaces;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Application.Awards.Commands.CreateAward;

public record CreateAwardCommand : IRequest<int>
{
    public int Year { get; set; }
    public string AwardName { get; set; }
    public string Contest { get; set; }
    public string Project { get; set; }
    public IFormFile Image { get; init; }
    public string ImageAlt { get; init; }
}
public class CreateAwardCommandHandler : IRequestHandler<CreateAwardCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostEnvironment _env;

    public CreateAwardCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }

    public async Task<int> Handle(CreateAwardCommand request, CancellationToken cancellationToken)
    {

        var entity = new Award();

        entity.Year = request.Year;
        entity.AwardName = request.AwardName;
        entity.Contest = request.Contest;
        entity.Project = request.Project;
        entity.ImageAlt = request.ImageAlt;

        if (request.Image != null)
        {
            if (!request.Image.CheckFileSize(1000))
                throw new FileException("File max size 1 mb");
            if (!request.Image.CheckFileType("image/"))
                throw new FileException("File type must be image");
            string newImageName = request.Image.GetRandomImagePath("Award");
            await _env.SaveAsync(request.Image, newImageName, cancellationToken);
            entity.ImagePath = newImageName;
        }
        await _unitOfWork.AwardRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}