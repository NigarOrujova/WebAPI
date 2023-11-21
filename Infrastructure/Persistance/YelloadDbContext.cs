using Application.Abstracts.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.Membership;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistance;

public partial class YelloadDbContext : IdentityDbContext<AppUser, AppRole, int, AppUserClaim, AppUserRole,
        AppUserLogin, AppRoleClaim, AppUserToken>, IYelloadDbContext
{
    public YelloadDbContext(DbContextOptions<YelloadDbContext> options) : base(options) { }

    public DbSet<OurValue> OurValues => Set<OurValue>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Media> Medias => Set<Media>();

    public DbSet<Portfolio> Portfolios => Set<Portfolio>();

    public DbSet<PortfolioImage> PortfolioImages => Set<PortfolioImage>();

    public DbSet<Team> Teams => Set<Team>();

    public DbSet<Footer> Footers => Set<Footer>();

    public DbSet<Home> Home => Set<Home>();

    public DbSet<We> We => Set<We>();

    public DbSet<Love> Love => Set<Love>();

    public DbSet<Esc> Esc => Set<Esc>();

    public DbSet<Contact> Contact => Set<Contact>();
    public DbSet<EmployeesPage> EmployeesPages => Set<EmployeesPage>();

    public DbSet<Blog> Blogs => Set<Blog>();

    public DbSet<Tag> Tags => Set<Tag>();

    public DbSet<BlogTagCloud> BlogTagClouds => Set<BlogTagCloud>();

    public DbSet<BlogMeta> blogMeta => Set<BlogMeta>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
