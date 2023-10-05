using Domain.Entities.Base;

namespace Domain.Entities;

public class OurValue:BaseEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
}
