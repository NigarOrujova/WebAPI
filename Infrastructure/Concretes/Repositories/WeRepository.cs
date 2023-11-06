using Application.Abstracts.Repositories;
using Domain.Entities;
using Infrastructure.Concretes.Repositories.Base;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Repositories;

public class WeRepository:Repository<We>,IWeRepository
{
    public WeRepository(YelloadDbContext context):base(context) { }
}
