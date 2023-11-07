using Domain.Entities.Base;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class Team:BaseEntity
{
    public string FulllName { get; set; } = null!;
    public string? FulllNameAz { get; set; }
    public string? Job { get; set; }
    public string? JobAz { get; set; }
    public string? ImagePath { get; set; }
    public IFormFile? Image { get; set; }
    public string? ImagePath2 { get; set; } 
    public IFormFile? Image2 { get; set; }
    public string? ImageAlt { get; set; }
    public string? ImageAltAz { get; set; }
}
