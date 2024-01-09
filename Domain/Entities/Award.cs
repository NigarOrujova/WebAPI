using Domain.Entities.Base;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Award:BaseEntity
{
    public int Year { get; set; }
    public string? AwardName { get; set; }
    public string? Contest { get; set; }
    public string? Project { get; set; }
    [NotMapped]
    public IFormFile? Image { get; set; }
    public string? ImagePath { get; set; }
    public string? ImageAlt { get; set; }
}
