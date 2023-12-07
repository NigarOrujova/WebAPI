using Application.Abstracts.Repositories;
using Domain.Entities;
using Infrastructure.Concretes.Repositories.Base;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Repositories;

internal class HeaderRepository : Repository<Header>, IHeaderRepository
{
    public HeaderRepository(YelloadDbContext context) : base(context) { }
}
