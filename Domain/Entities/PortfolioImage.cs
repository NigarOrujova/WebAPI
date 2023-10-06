using Domain.Entities.Base;

namespace Domain.Entities;

public class PortfolioImage:BaseEntity
{
    public string ImagePath { get; set; } = null!;
    public string ImageAlt { get; set; } = null!;
    public bool IsMain { get; set; }
    public int? PortfolioId { get; set; }
    public Portfolio? Portfolio { get; set; }
}
