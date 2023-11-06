using Application.Abstracts.Repositories;
using Domain.Entities;
using Infrastructure.Concretes.Repositories.Base;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Repositories;

public class HomeRepository:Repository<Home>,IHomeRepository
{
    public HomeRepository(YelloadDbContext context):base(context) { }
}
