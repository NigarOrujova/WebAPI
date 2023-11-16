using Domain.Entities.Base;

namespace Domain.Entities;

public class Footer:BaseEntity
{
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? AddressAz { get; set; }
    public ICollection<Media>? Medias { get; set; }
}
