using Domain.Entities.Base;

namespace Domain.Entities;

public class Portfolio:BaseEntity
{
    public string? Title { get; set; }
    public string? TitleAz { get; set; }
    public string? SubTitle { get; set; }
    public string? SubTitleAz { get; set; }
    public string? Description { get; set; }
    public string? DescriptionAz { get; set; }
    public bool IsMain { get; set; }
    public List<int>? CategoryIds { get; set; } = new List<int>();
    public List<PortfolioCategory>? PortfolioCategories { get; set; }
    public ICollection<PortfolioImage>? Images { get; set; }
}
