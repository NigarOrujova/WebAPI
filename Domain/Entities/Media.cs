using Domain.Entities.Base;

namespace Domain.Entities;

public class Media:BaseEntity
{
    public string Title { get; set; } = null!;
    public string URL { get; set; }= null!;
    public int? FooterId { get; set; }
    public Footer? Footer { get; set; }
}
