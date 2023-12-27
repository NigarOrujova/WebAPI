using Domain.Entities.Base;

namespace Domain.Entities;

public class Portfolio:BaseEntity
{
    public string? Link { get; set; }
    public int? Sound { get; set; }
    public string Title { get; set; }
    public string TitleAz { get; set; }
    public string? SubTitle { get; set; }
    public string? SubTitleAz { get; set; }
    public string? Description { get; set; }
    public string? DescriptionAz { get; set; }
    public string? Slug { get; set; }
    public string? SlugAz { get; set; }
    public bool IsMain { get; set; }
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
    public List<int>? CategoryIds { get; set; } = new List<int>();
    public List<PortfolioCategory>? PortfolioCategories { get; set; }
    public List<int>? ImageIds { get; set; } = new List<int>();
    public List<PortfolioImage>? Images { get; set; }
}
