using Application.Abstracts.Repositories;
using Domain.Entities;
using Infrastructure.Concretes.Repositories.Base;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Repositories;

public class TeamRepository : Repository<Team>, ITeamRepository
{
    public TeamRepository(YelloadDbContext context) : base(context) { }
}
