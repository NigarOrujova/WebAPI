using Application.Abstracts.Repositories;
using Domain.Entities;
using Infrastructure.Concretes.Repositories.Base;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Repositories;

public class FooterRepository : Repository<Footer>, IFooterRepository
{
    public FooterRepository(YelloadDbContext context) : base(context) { }
}