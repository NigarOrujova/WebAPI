using Infrastructure.Identity.Users.Commands;
using Infrastructure.Identity.Users.Queries;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers;

public class UsersController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
        => Ok(await Mediator.Send(new UserAllQuery()));
    [HttpPost("setrole")]
    public async Task<IActionResult> SetRoleAsync(UserSetRoleCommand request)
   => Ok(await Mediator.Send(request));
    [HttpPost("setprincipal")]
    public async Task<IActionResult> SetPrincipalAsync(UserSetPrincipalCommand request)
   => Ok(await Mediator.Send(request));
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(UserDeleteCommand request)
   => Ok(await Mediator.Send(request));
}
