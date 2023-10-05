using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstracts.Common;

public interface IYelloadDbContext
{
    DbSet<OurValue> OurValues { get;}
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
