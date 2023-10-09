using Application.Abstracts.Repositories;
using Domain.Entities;
using Infrastructure.Concretes.Repositories.Base;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Repositories;

public class PortfolioImageRepository : Repository<PortfolioImage>, IPortfolioImageRepository
{
    public PortfolioImageRepository(YelloadDbContext context) : base(context) { }
}