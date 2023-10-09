using Application.Extensions;
using Domain.Entities.Membership;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Infrastructure.Identity.Accounts.Commands;

public class SigninCommand : IRequest<AppUser>
{
    public string UserName { get; set; }
    public string Password { get; set; }


    public class SigninCommandHandler : IRequestHandler<SigninCommand, AppUser>
    {
        private readonly SignInManager<AppUser> signinManager;
        private readonly IActionContextAccessor ctx;

        public SigninCommandHandler(SignInManager<AppUser> signinManager,IActionContextAccessor ctx)
        {
            this.signinManager = signinManager;
            this.ctx = ctx;
        }


        public async Task<AppUser> Handle(SigninCommand request, CancellationToken cancellationToken)
        {
            AppUser user = null;


            if (request.UserName.IsEmail())
            {
                user = await signinManager.UserManager.FindByEmailAsync(request.UserName);
            }
            else
            {
                user = await signinManager.UserManager.FindByNameAsync(request.UserName);
            }


            if (user == null)
            {
                ctx.ActionContext.ModelState.AddModelError("UserName","Istifadeci adi ve ya sifre sehvdir");

                return null;
            }

            var result = await signinManager.CheckPasswordSignInAsync(user,request.Password,true);


            if (result.IsLockedOut)
            {
                ctx.ActionContext.ModelState.AddModelError("UserName", "Hesabibiz kecici olaraq mehdudlashdirilib");

                return null;
            }


            if (result.IsNotAllowed)
            {
                ctx.ActionContext.ModelState.AddModelError("UserName", "Hesaba daxil olmaq mumkun deyil");

                return null;
            }



            if (!user.EmailConfirmed)
            {
                ctx.ActionContext.ModelState.AddModelError("UserName", "Hesabiniz tesdiq edilmeyib");

                return null;
            }


            if (result.Succeeded)
            {
                return user;
            }

            ctx.ActionContext.ModelState.AddModelError("UserName", "Istifadeci adi ve ya sifre sehvdir");
            return null;
        }
    }
}
