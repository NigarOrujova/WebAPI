using Domain.Entities.Base;

namespace Domain.Entities;

public class OurValue:BaseEntity
{
    public string Title { get; set; } = null!;
    public string? TitleAz { get; set; }
    public string Description { get; set; } = null!;
    public string? DescriptionAz { get; set; }
}
