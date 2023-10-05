using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstracts.Common;

public interface IYelloadDbContext
{
    public DbSet<OurValue> OurValues { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
