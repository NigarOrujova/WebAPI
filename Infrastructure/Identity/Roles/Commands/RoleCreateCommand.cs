using Application.Extensions;
using Domain.Entities.Membership;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Infrastructure.Identity.Roles.Commands;

public class RoleCreateCommand : IRequest<AppRole>
{
    public string Name { get; set; }


    public class RoleCreateCommandHandler : IRequestHandler<RoleCreateCommand, AppRole>
    {
        private readonly RoleManager<AppRole> roleManager;
        private readonly IActionContextAccessor ctx;

        public RoleCreateCommandHandler(RoleManager<AppRole> roleManager,IActionContextAccessor ctx)
        {
            this.roleManager = roleManager;
            this.ctx = ctx;
        }


        public async Task<AppRole> Handle(RoleCreateCommand request, CancellationToken cancellationToken)
        {
            var role = new AppRole
            {
                Name = request.Name
            };

            var result = await roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ctx.AddModelError("Name", error.Description);
                }

                return null;
            }


            return role;
        }
    }
}
