using Domain.Entities.Base;

namespace Domain.Entities;

public class Tag:BaseEntity
{
    public string Name { get; set; }
    public string NameAz { get; set; }
    public virtual ICollection<BlogTagCloud> TagCloud { get; set; }
}
