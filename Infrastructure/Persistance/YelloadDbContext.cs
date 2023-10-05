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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
