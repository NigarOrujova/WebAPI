using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstracts.Common.Interfaces;

public interface IYelloadDbContext
{
    DbSet<OurValue> OurValues { get; }
    DbSet<Category> Categories { get; }
    DbSet<Customer> Customers { get; }
    DbSet<Media> Medias { get; }
    DbSet<Portfolio> Portfolios { get; }
    DbSet<PortfolioImage> PortfolioImages { get; }
    DbSet<Team> Teams { get; }
}
