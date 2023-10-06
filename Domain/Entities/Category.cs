using Domain.Entities.Base;

namespace Domain.Entities;

public class Category:BaseEntity
{
    public string Name { get; set; } = null!;
    public int? PortfolioId { get; set; }
    public Portfolio? Portfolio { get; set; }
}
