using Domain.Entities.Base;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class Customer:BaseEntity
{
    public string? ImagePath { get; set; }
    public IFormFile? Image { get; set; } 
    public string? ImageAlt { get; set; }
    public string? ImageAltAz { get; set; }
    public byte? Rank { get; set; }
}
