using Application.Abstracts.Common.Interfaces;
using Domain.Entities.Membership;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text.RegularExpressions;

namespace Infrastructure.Identity.Accounts.Commands;

public class RegisterConfirmationCommand : IRequest<AppUser>
{
    public string Token { get; set; }

    public class RegisterConfirmationCommandHandler : IRequestHandler<RegisterConfirmationCommand, AppUser>
    {
        private readonly UserManager<AppUser> userManager;
        private readonly ICryptoService cryptoService;
        private readonly IActionContextAccessor ctx;

        public RegisterConfirmationCommandHandler(UserManager<AppUser> userManager,
            ICryptoService cryptoService,
            IActionContextAccessor ctx)
        {
            this.userManager = userManager;
            this.cryptoService = cryptoService;
            this.ctx = ctx;
        }

        public async Task<AppUser> Handle(RegisterConfirmationCommand request, CancellationToken cancellationToken)
        {
            var plainToken = cryptoService.Decrypt(request.Token); // 1-dafdfwefwsadsaasd

            var match = Regex.Match(plainToken, @"(?<id>\d+)-(?<token>.+)");

            if (!match.Success)
            {
                ctx.ActionContext.ModelState.AddModelError("Token", "Token zedelenib!");
                return null;
            }

            string token = match.Groups["token"].Value;

            var user = await userManager.FindByIdAsync(match.Groups["id"].Value);

            if (user == null)
            {
                ctx.ActionContext.ModelState.AddModelError("Token", "Istifadeci movcud deyil!");
                return null;
            }
            else if (user.EmailConfirmed)
            {
                ctx.ActionContext.ModelState.AddModelError("Token", "Istifadeci artiq tesdiq edilib!");
                return null;
            }
            var response = await userManager.ConfirmEmailAsync(user, token);

            if (!response.Succeeded)
            {
                foreach (var item in response.Errors)
                {
                    ctx.ActionContext.ModelState.AddModelError("Token", item.Description);
                }

                return null;
            }


            return user;
        }
    }
}
