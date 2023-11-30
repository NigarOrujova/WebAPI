using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Tag:BaseEntity
{
    public string? Name { get; set; }
    public string? NameAz { get; set; }
    [NotMapped]
    public List<int>? BlogIds { get; set; } = new List<int>();
    public virtual List<BlogTagCloud>? TagCloud { get; set; }
}
