using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Repositories.Base;

public class Repository
{
    private readonly YelloadDbContext _context;

    public Repository(YelloadDbContext context)
    {
        _context = context;
    }
}
