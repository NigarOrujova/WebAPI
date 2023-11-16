using Application.Abstracts.Repositories;
using Domain.Entities;
using Infrastructure.Concretes.Repositories.Base;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Repositories;

public class BlogRepository : Repository<Blog>, IBlogRepository
{
    public BlogRepository(YelloadDbContext context) : base(context) { }
}
