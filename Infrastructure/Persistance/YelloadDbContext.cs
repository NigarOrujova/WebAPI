using Application.Abstracts.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistance;

public partial class YelloadDbContext : IdentityDbContext, IYelloadDbContext
{
    public YelloadDbContext(DbContextOptions<YelloadDbContext> options) : base(options) { }

    public DbSet<OurValue> OurValues => Set<OurValue>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Media> Medias => Set<Media>();

    public DbSet<Portfolio> Portfolios => Set<Portfolio>();

    public DbSet<PortfolioImage> PortfolioImages => Set<PortfolioImage>();

    public DbSet<Team> Teams => Set<Team>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
