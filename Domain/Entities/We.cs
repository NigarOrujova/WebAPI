using Domain.Entities.Base;

namespace Domain.Entities;

public class We:BaseEntity
{
    public string? MetaKeyword { get; set; }
    public string? MetaTitle { get; set; }
    public string? OgTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? OgDescription { get; set; }
    public string? OgSiteName { get; set; }
    public string? MobileTitle { get; set; }
    public string? AppName { get; set; }
}
