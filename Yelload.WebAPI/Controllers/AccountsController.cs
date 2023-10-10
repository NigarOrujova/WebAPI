using Application.Abstracts.Common.Interfaces;
using Infrastructure.Identity.Accounts.Commands;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers;

public class AccountsController : ApiControllerBase
{
    private ITokenService? _tokenService;
    protected ITokenService TokenService => _tokenService ??= HttpContext.RequestServices.GetService<ITokenService>()!;

    [HttpPost("signin")]
    public async Task<IActionResult> Signin([FromForm]SigninCommand command)
    {
        var user = await Mediator.Send(command);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var token = TokenService.BuildToken(user);

        return Ok(new
        {
            error = false,
            accessToken = token
        });
    }
    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromForm]RegisterCommand command)
    {
        var user = await Mediator.Send(command);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(new
        {
            error = false,
            message = "Signup successful"
        });
    }

}
