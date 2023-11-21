using Application.Abstracts.Common.Exceptions;
using Application.Abstracts.Common.Interfaces;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Application.Employees.Commands;

public record UpdateEmployeeCommand:IRequest<EmployeesPage>
{
    public string? Title { get; set; }
    public string? TitleAz { get; set; }
    public string? SubTitle { get; set; }
    public string? SubTitleAz { get; set; }
    public string? Description { get; set; }
    public string? DescriptionAz { get; set; }
    public string? Title2 { get; set; }
    public string? TitleAz2 { get; set; }
    public string? SubTitle2 { get; set; }
    public string? SubTitleAz2 { get; set; }
    public string? Title3 { get; set; }
    public string? TitleAz3 { get; set; }
    public string? Description2 { get; set; }
    public string? DescriptionAz2 { get; set; }
    public string? FullName { get; set; }
    public string? FullNameAz { get; set; }
    public IFormFile? Image { get; set; }
    public string? ImageAlt { get; set; }
    public string? ImageAltAz { get; set; }
    public string? FullName2 { get; set; }
    public string? FullNameAz2 { get; set; }
    public IFormFile? Image2 { get; set; }
    public string? ImageAlt2 { get; set; }
    public string? ImageAltAz2 { get; set; }
}
public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, EmployeesPage>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostEnvironment _env;

    public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork, IHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }

    public async Task<EmployeesPage> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        EmployeesPage entity = await _unitOfWork.EmployeesPageRepository.GetAsync()
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
        string newImageName = request.Image.GetRandomImagePath("about");

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
        string newImageName2 = request.Image2.GetRandomImagePath("about");

        _env.ArchiveImage(entity.ImagePath2);
        await _env.SaveAsync(request.Image2, newImageName2, cancellationToken);

        entity.ImagePath2 = newImageName2;

    saveimg:

        entity.Title = request.Title;
        entity.TitleAz = request.TitleAz;
        entity.SubTitleAz = request.SubTitleAz;
        entity.DescriptionAz = request.DescriptionAz;
        entity.TitleAz2 = request.TitleAz2;
        entity.TitleAz3 = request.TitleAz3;
        entity.SubTitleAz2 = request.SubTitleAz2;
        entity.DescriptionAz2 = request.DescriptionAz2;
        entity.FullNameAz = request.FullNameAz;
        entity.ImageAltAz = request.ImageAltAz;
        entity.ImageAltAz2 = request.ImageAltAz2;


        await _unitOfWork.EmployeesPageRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }
}
