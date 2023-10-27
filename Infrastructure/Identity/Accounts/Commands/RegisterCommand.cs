using Application.Abstracts.Common.Interfaces;
using Domain.Entities.Membership;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Infrastructure.Identity.Accounts.Commands;

public class RegisterCommand : IRequest<AppUser>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AppUser>
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IActionContextAccessor ctx;
        private readonly IEmailService emailService;
        private readonly ICryptoService cryptoService;

        public RegisterCommandHandler(UserManager<AppUser> userManager, IActionContextAccessor ctx,
            IEmailService emailService, ICryptoService cryptoService)
        { 
            this.userManager = userManager;
            this.ctx = ctx;
            this.emailService = emailService;
            this.cryptoService = cryptoService;
        }

        public async Task<AppUser> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);


            if (user != null)
            {
                ctx.ActionContext.ModelState.AddModelError("Email", "Bu e-poct artiq istifade olunub");
                return null;
            }


            user = new AppUser
            {
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                UserName = $"{request.Name}-{Guid.NewGuid()}".ToLower(),
                EmailConfirmed=true
            };
            bool isExistUsername = userManager.Users.Any(us => us.UserName == user.UserName);
            if (isExistUsername)
            {
                ctx.ActionContext.ModelState.AddModelError(String.Empty,
                    "Bu İstifadəçi adı artıq mövcuddur. Başqa İstifadəçi adı istifadə edin");
                return null;
            }

            bool isExistEmail = userManager.Users.Any(us => us.Email == user.Email);
            if (isExistEmail)
            {
                ctx.ActionContext.ModelState.AddModelError(String.Empty, "Bu Email artıq mövcuddur. Başqa Email istifadə edin");
                return null;
            }

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ctx.ActionContext.ModelState.AddModelError("Name", item.Description);
                }

                return null;
            }

            return user;
        }
    }
}
