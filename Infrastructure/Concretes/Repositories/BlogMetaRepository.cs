using Application.Abstracts.Repositories;
using Domain.Entities;
using Infrastructure.Concretes.Repositories.Base;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Repositories;

public class BlogMetaRepository : Repository<BlogMeta>, IBlogMetaRepository
{
    public BlogMetaRepository(YelloadDbContext context) : base(context) { }
}