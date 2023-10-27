using Domain.Entities.Base;

namespace Domain.Entities;

public class Category:BaseEntity
{
    public string Name { get; set; } = null!;
    public List<PortfolioCategory>? PortfolioCategories { get; set; }
}
