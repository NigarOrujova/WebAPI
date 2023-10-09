using Application.Extensions;
using Domain.Entities.Membership;
using Infrastructure.Concretes.Common;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Users.Commands;

public class UserSetRoleCommand : IRequest<JsonResponse>
{
    public int UserId { get; set; }
    public string RoleName { get; set; }
    public bool Selected { get; set; }



    public class UserSetRoleCommandHandler : IRequestHandler<UserSetRoleCommand, JsonResponse>
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly IActionContextAccessor ctx;
        private readonly YelloadDbContext db;

        public UserSetRoleCommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,
            IActionContextAccessor ctx,
            YelloadDbContext db)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.ctx = ctx;
            this.db = db;
        }
        public async Task<JsonResponse> Handle(UserSetRoleCommand request, CancellationToken cancellationToken)
        {
            var response = new JsonResponse
            {
                Error = false
            };

            var user = await userManager.Users.FirstOrDefaultAsync(m => m.Id == request.UserId, cancellationToken);


            if (user == null)
            {
                response.Error = true;
                response.Message = "İstifadəçi mövcud deyil";
                goto end;
            }
            else if (user.EmailConfirmed == false)
            {
                response.Error = true;
                response.Message = "İstifadəçi hesabı təsdiq etməyib";
                goto end;
            }

            var role = await roleManager.FindByNameAsync(request.RoleName);

            if (role == null)
            {
                response.Error = true;
                response.Message = $"{request.RoleName} mövcud deyil";
                goto end;
            }

            var executorId = ctx.GetUserId();

            var availableThisRole = await (from ur in db.UserRoles
                                       join r in db.Roles on ur.RoleId equals r.Id
                                       where ur.UserId == executorId && r.Rank > role.Rank
                                       select 0).AnyAsync(cancellationToken);

            if (!availableThisRole)
            {
                response.Error = true;
                response.Message = "Bu rol üçün əməliyyat icazəniz yoxdur";
                goto end;
            }

            IdentityResult result;
            if (request.Selected)
            {
                result = await userManager.AddToRoleAsync(user, request.RoleName);

                if (!result.Succeeded)
                {
                    response.Error = true;
                    response.Message = result.Errors.FirstOrDefault()?.Description;
                    goto end;
                }

                response.Message = $"`{user.Name} {user.Surname}` {request.RoleName}`a əlavə edildi";
            }
            else
            {
                result = await userManager.RemoveFromRoleAsync(user, request.RoleName);

                if (!result.Succeeded)
                {
                    response.Error = true;
                    response.Message = result.Errors.FirstOrDefault()?.Description;
                    goto end;
                }

                response.Message = $"`{user.Name} {user.Surname}` {request.RoleName}`dan çıxarıldı";
            }


        end:
            return response;
        }
    }
}
