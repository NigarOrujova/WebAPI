using Application.Abstracts.Repositories;
using Domain.Entities;
using Infrastructure.Concretes.Repositories.Base;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Repositories;

public class TagRepository : Repository<Tag>, ITagRepository
{
    public TagRepository(YelloadDbContext context) : base(context) { }
}