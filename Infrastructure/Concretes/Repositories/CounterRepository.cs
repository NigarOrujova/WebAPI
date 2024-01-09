using Application.Abstracts.Repositories;
using Domain.Entities;
using Infrastructure.Concretes.Repositories.Base;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Repositories;

public class CounterRepository : Repository<Counter>, ICounterRepository
{
    public CounterRepository(YelloadDbContext context) : base(context) { }
}