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
    private IHomeRepository? _homeRepository;
    private IContactRepository? _contactRepository;
    private IEscRepository? _escRepository;
    private ILoveRepository? _loveRepository;
    private IWeRepository? _weRepository;
    private IEmployeesPageRepository? _employeesPageRepository;
    private IBlogRepository? _blogRepository;
    private IBlogTagCloudRepository? _blogTagCloudRepository;
    private ITagRepository? _tagRepository;
    private IBlogMetaRepository? _blogMetaRepository;
    private IHeaderRepository? _headerRepository;
    public IOurValueRepository OurValueRepository => _ourValueRepository ??= new OurValueRepository(_dbContext);

    public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_dbContext);

    public IMediaRepository MediaRepository => _mediaRepository ??= new MediaRepository(_dbContext);

    public ICustomerRepository CustomerRepository => _customerRepository ??= new CustomerRepository(_dbContext);

    public IPortfolioImageRepository PortfolioImageRepository => _portfolioImageRepository ??= new PortfolioImageRepository(_dbContext);

    public IPortfolioRepository PortfolioRepository => _portfolioRepository ??= new PortfolioRepository(_dbContext);

    public ITeamRepository TeamRepository => _teamRepository ??= new TeamRepository(_dbContext);

    public IFooterRepository FooterRepository => _footerRepository ??= new FooterRepository(_dbContext);

    public IContactRepository ContactRepository => _contactRepository ??=new ContactRepository(_dbContext);

    public IHomeRepository HomeRepository => _homeRepository ??= new HomeRepository(_dbContext);

    public IEscRepository EscRepository => _escRepository ??= new EscRepository(_dbContext);

    public ILoveRepository LoveRepository => _loveRepository ??= new LoveRepository(_dbContext);

    public IWeRepository WeRepository => _weRepository ??= new WeRepository(_dbContext);

    public IEmployeesPageRepository EmployeesPageRepository => _employeesPageRepository ??= new EmployeesPageRepository(_dbContext);

    public IBlogRepository BlogRepository => _blogRepository ??=new BlogRepository(_dbContext);

    public IBlogTagCloudRepository BlogTagCloudRepository => _blogTagCloudRepository ??=new BlogTagCloudRepository(_dbContext);

    public ITagRepository TagRepository => _tagRepository ??= new TagRepository(_dbContext);
    public IBlogMetaRepository BlogMetaRepository => _blogMetaRepository ??= new BlogMetaRepository(_dbContext);

    public IHeaderRepository HeaderRepository => _headerRepository ??=new HeaderRepository(_dbContext);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _dbContext.SaveChangesAsync(cancellationToken);
}
