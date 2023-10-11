using Domain.Entities.Base;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class EmployeesPage:BaseEntity
{
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public string? Description { get; set; }
    public string? Title2 { get; set; }
    public string? SubTitle2 { get; set; }
    public string? Title3 { get; set; }
    public string? Description2 { get; set;}
    public string? FullName { get; set; }
    public IFormFile? Image { get; set; }
    public string? ImagePath { get; set; }
    public string? ImageAlt { get; set; }
    public string? FullName2 { get; set; }
    public IFormFile? Image2 { get; set; }
    public string? ImagePath2 { get; set; }
    public string? ImageAlt2 { get; set; }
}
