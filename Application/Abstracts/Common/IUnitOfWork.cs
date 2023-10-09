using Application.Abstracts.Repositories;

namespace Application.Abstracts.Common;

public interface IUnitOfWork
{
    ICategoryRepository CategoryRepository { get; }
    IMediaRepository MediaRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    IPortfolioImageRepository PortfolioImageRepository { get; }
    IPortfolioRepository PortfolioRepository { get; }
    ITeamRepository TeamRepository { get; }
    IOurValueRepository OurValueRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
