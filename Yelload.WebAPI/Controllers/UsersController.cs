using Infrastructure.Identity.Users.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers;
[Authorize(Roles = "sa")]
public class UsersController : ApiControllerBase
{
    [HttpPost("setrole")]
    public async Task<IActionResult> SetRoleAsync([FromForm] UserSetRoleCommand request)
   => Ok(await Mediator.Send(request));
    [HttpPost("setprincipal")]
    public async Task<IActionResult> SetPrincipalAsync([FromForm] UserSetPrincipalCommand request)
   => Ok(await Mediator.Send(request));
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromForm] UserDeleteCommand request)
   => Ok(await Mediator.Send(request));
}
