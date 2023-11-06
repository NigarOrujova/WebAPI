using Application.Abstracts.Repositories;
using Domain.Entities;
using Infrastructure.Concretes.Repositories.Base;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Repositories;

public class EscRepository:Repository<Esc>,IEscRepository
{
    public EscRepository(YelloadDbContext context):base(context) { }
}
