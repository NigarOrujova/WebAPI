using Domain.Entities.Base;

namespace Domain.Entities
{
    public class PortfolioCategory:BaseEntity
    {
        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
