using Domain.Entities.Base;

namespace Domain.Entities;

public class Team:BaseEntity
{
    public string FulllName { get; set; } = null!;
    public string? Job { get; set; }
    public string? ImagePath { get; set; }
    public string? ImagePath2 { get; set; } 
    public string? ImageAlt { get; set; }
}
