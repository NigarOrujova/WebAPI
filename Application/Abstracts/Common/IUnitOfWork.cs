using Application.Abstracts.Repositories;

namespace Application.Abstracts.Common;

public interface IUnitOfWork
{
    IOurValueRepository OurValueRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
