using Application.Abstracts.Repositories;
using Domain.Entities;
using Infrastructure.Concretes.Repositories.Base;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Repositories;

public class LoveRepository:Repository<Love>,ILoveRepository
{
    public LoveRepository(YelloadDbContext context):base(context) { }
}
