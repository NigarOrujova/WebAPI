namespace Domain.Entities.Base;

public class BaseEntity
{
    public int Id { get; set; }
    public bool DeletedAt { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
}
