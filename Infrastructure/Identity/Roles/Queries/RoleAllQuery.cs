using Domain.Entities.Membership;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Roles.Queries;

public record RoleAllQuery:IRequest<IEnumerable<AppRole>>;
public class RoleAllQueryHandler : IRequestHandler<RoleAllQuery, IEnumerable<AppRole>>
{
    private readonly YelloadDbContext _db;

    public RoleAllQueryHandler(YelloadDbContext db)
    {
        _db = db;
    }
    public async Task<IEnumerable<AppRole>> Handle(RoleAllQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<AppRole> appRole = await _db.Roles.ToListAsync(cancellationToken)
          ?? throw new NotImplementedException();
        return appRole;
    }
}
