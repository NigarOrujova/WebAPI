using Infrastructure.Identity.Accounts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Infrastructure.Identity.Providers;

public class AppClaimProvider : IClaimsTransformation
{
    static public string[] principals = null;
    private readonly IMediator mediator;

    public AppClaimProvider(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.Identity.IsAuthenticated)
        {
            var query = new ReloadAuthorityQuery
            {
                User = principal
            };

            await mediator.Send(query);
        }


        return principal;
    }
}
