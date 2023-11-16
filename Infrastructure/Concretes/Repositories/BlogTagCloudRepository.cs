using Application.Abstracts.Repositories;
using Domain.Entities;
using Infrastructure.Concretes.Repositories.Base;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Repositories;

public class BlogTagCloudRepository : Repository<BlogTagCloud>, IBlogTagCloudRepository
{
    public BlogTagCloudRepository(YelloadDbContext context) : base(context) { }
}