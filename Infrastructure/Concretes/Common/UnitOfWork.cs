using Application.Abstracts.Common;
using Application.Abstracts.Repositories;
using Infrastructure.Concretes.Repositories;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Common;

public class UnitOfWork : IUnitOfWork
{
    readonly YelloadDbContext _dbContext;

    public UnitOfWork(YelloadDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    private IOurValueRepository? _ourValueRepository;
    public IOurValueRepository OurValueRepository => _ourValueRepository ??= new OurValueRepository(_dbContext);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _dbContext.SaveChangesAsync(cancellationToken);
}
