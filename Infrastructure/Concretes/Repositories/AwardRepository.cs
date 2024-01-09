using Application.Abstracts.Repositories;
using Domain.Entities;
using Infrastructure.Concretes.Repositories.Base;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Repositories;

public class AwardRepository : Repository<Award>, IAwardRepository
{
    public AwardRepository(YelloadDbContext context) : base(context) { }
}