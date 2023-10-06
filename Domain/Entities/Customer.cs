using Domain.Entities.Base;

namespace Domain.Entities;

public class Customer:BaseEntity
{
    public string ImagePath { get; set; } = null!;
    public string ImageAlt { get; set; } = null!;
}
