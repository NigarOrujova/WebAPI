using Application.Abstracts.Common.Interfaces;
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
    private ICategoryRepository? _categoryRepository;
    private IMediaRepository? _mediaRepository;
    private ICustomerRepository? _customerRepository;
    private IPortfolioRepository? _portfolioRepository;
    private IPortfolioImageRepository? _portfolioImageRepository;
    private ITeamRepository? _teamRepository;
    private IFooterRepository? _footerRepository;
    public IOurValueRepository OurValueRepository => _ourValueRepository ??= new OurValueRepository(_dbContext);

    public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_dbContext);

    public IMediaRepository MediaRepository => _mediaRepository ??= new MediaRepository(_dbContext);

    public ICustomerRepository CustomerRepository => _customerRepository ??= new CustomerRepository(_dbContext);

    public IPortfolioImageRepository PortfolioImageRepository => _portfolioImageRepository ??= new PortfolioImageRepository(_dbContext);

    public IPortfolioRepository PortfolioRepository => _portfolioRepository ??= new PortfolioRepository(_dbContext);

    public ITeamRepository TeamRepository => _teamRepository ??= new TeamRepository(_dbContext);

    public IFooterRepository FooterRepository => _footerRepository ??= new FooterRepository(_dbContext);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _dbContext.SaveChangesAsync(cancellationToken);
}
