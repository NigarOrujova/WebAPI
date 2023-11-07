using Domain.Entities.Base;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class EmployeesPage:BaseEntity
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
    public string? Description2 { get; set;}
    public string? DescriptionAz2 { get; set; }
    public string? FullName { get; set; }
    public string? FullNameAz { get; set; }
    public IFormFile? Image { get; set; }
    public string? ImagePath { get; set; }
    public string? ImageAlt { get; set; }
    public string? ImageAltAz { get; set; }
    public string? FullName2 { get; set; }
    public string? FullNameAz2 { get; set; }
    public IFormFile? Image2 { get; set; }
    public string? ImagePath2 { get; set; }
    public string? ImageAlt2 { get; set; }
    public string? ImageAltAz2 { get; set; }
}
