using Application.Abstracts.Repositories;
using Domain.Entities;
using Infrastructure.Concretes.Repositories.Base;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Repositories;

public class OurValueRepository : Repository<OurValue>, IOurValueRepository
{
    public OurValueRepository(YelloadDbContext context) : base(context) { }
}