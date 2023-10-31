using Domain.Entities.Base;

namespace Domain.Entities;

public class Category:BaseEntity
{
    public string Name { get; set; } = null!;
    public List<int>? PortfolioIds { get; set; } = new List<int>();
    public List<PortfolioCategory>? PortfolioCategories { get; set; }
}
