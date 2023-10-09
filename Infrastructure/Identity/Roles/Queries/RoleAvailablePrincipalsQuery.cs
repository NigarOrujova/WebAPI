using Infrastructure.Identity.Dtos.Roles;
using Infrastructure.Identity.Providers;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Roles.Queries;

public class RoleAvailablePrincipalsQuery : IRequest<IEnumerable<AvailablePrincipal>>
{
    public int RoleId { get; set; }


    public class RoleAvailablePrincipalsQueryHandler : IRequestHandler<RoleAvailablePrincipalsQuery, IEnumerable<AvailablePrincipal>>
    {
        private readonly YelloadDbContext db;

        public RoleAvailablePrincipalsQueryHandler(YelloadDbContext db)
        {
            this.db = db;
        }
        public async Task<IEnumerable<AvailablePrincipal>> Handle(RoleAvailablePrincipalsQuery request, CancellationToken cancellationToken)
        {
            var principals = AppClaimProvider.principals ?? new string[] { };


            var claims = await db.RoleClaims.Where(m => m.RoleId == request.RoleId && m.ClaimValue.Equals("1"))
                .Select(m => m.ClaimType)
                .ToArrayAsync(cancellationToken);


            var result = (from p in principals
                          join c in claims on p equals c into l_join_principals
                          from lp in l_join_principals.DefaultIfEmpty()
                          select new AvailablePrincipal
                          {
                              PrincipalName = p,
                              Selected = lp != null
                          }).AsEnumerable();

            return result;
        }
    }
}
