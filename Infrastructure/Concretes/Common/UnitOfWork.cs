using Application.Abstracts.Common;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Common;

public class UnitOfWork : IUnitOfWork
{
    readonly YelloadDbContext _dbContext;

    public UnitOfWork(YelloadDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _dbContext.SaveChangesAsync(cancellationToken);
}
