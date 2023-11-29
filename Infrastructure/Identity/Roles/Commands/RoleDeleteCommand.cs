using Domain.Dtos;
using Domain.Entities.Membership;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Roles.Commands;

public class RoleDeleteCommand : IRequest<JsonResponse>
{
    public string Id { get; set; }
    public class RoleDeleteCommandHandler : IRequestHandler<RoleDeleteCommand, JsonResponse>
    {
        private readonly RoleManager<AppRole> roleManager;

        public RoleDeleteCommandHandler(RoleManager<AppRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task<JsonResponse> Handle(RoleDeleteCommand request, CancellationToken cancellationToken)
        {
            AppRole user = await roleManager.FindByIdAsync(request.Id);
            if (user is null)
            {
                return null;
            }
            else
            {
                IdentityResult result = await roleManager.DeleteAsync(user);
                return new JsonResponse
                {
                    Status="true",
                    Message = "Success"
                };
            }
        }
    }
}
