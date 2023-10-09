using Domain.Entities.Membership;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Roles.Queries;

public class RoleSingleQuery : IRequest<AppRole>
{
    public int Id { get; set; }


    public class RoleSingleQueryHandler : IRequestHandler<RoleSingleQuery, AppRole>
    {
        private readonly RoleManager<AppRole> roleManager;

        public RoleSingleQueryHandler(RoleManager<AppRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task<AppRole> Handle(RoleSingleQuery request, CancellationToken cancellationToken)
        {
            if (request.Id < 1)
                return null;


            var model = await roleManager.Roles.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

            return model;
        }
    }
}
