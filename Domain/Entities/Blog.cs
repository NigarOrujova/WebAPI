using Domain.Entities.Base;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class Blog:BaseEntity
{
    public string Title { get; set; }
    public string TitleAz { get; set; }
    public string Description { get; set; }
    public string DescriptionAz { get; set; }
    public string ImagePath { get; set; }
    public IFormFile Image { get; set; }
    public string ImageAlt { get; set; }
    public string ImageAltAz { get; set; }
    public string Slug { get; set; } 
    public string? MetaKeyword { get; set; }
    public string? MetaKeywordAz { get; set; }
    public string? MetaTitle { get; set; }
    public string? MetaTitleAz { get; set; }
    public string? OgTitle { get; set; }
    public string? OgTitleAz { get; set; }
    public string? MetaDescription { get; set; }
    public string? MetaDescriptionAz { get; set; }
    public string? OgDescription { get; set; }
    public string? OgDescriptionAz { get; set; }
    public string? MobileTitle { get; set; }
    public string? MobileTitleAz { get; set; }
    public DateTime? PublishDate { get; set; }
    public virtual List<BlogTagCloud> TagCloud { get; set; }
    public List<int>? TagIds { get; set; } = new List<int>();
}
