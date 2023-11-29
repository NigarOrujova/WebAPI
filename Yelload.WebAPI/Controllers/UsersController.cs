using Infrastructure.Identity.Users.Commands;
using Infrastructure.Identity.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers;

public class UsersController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
        => Ok(await Mediator.Send(new UserAllQuery()));
    [HttpPost("setrole")]
    public async Task<IActionResult> SetRoleAsync([FromForm]UserSetRoleCommand request)
   => Ok(await Mediator.Send(request));
    [HttpPost("setprincipal")]
    public async Task<IActionResult> SetPrincipalAsync([FromForm] UserSetPrincipalCommand request)
   => Ok(await Mediator.Send(request));
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var data=id.ToString();
       return Ok(await Mediator.Send(new UserDeleteCommand(data)));
    }
    [HttpGet("details")]
    public async Task<IActionResult> GetDetails([FromQuery] UserDetailQuery query)
    {
        var availableRoles = await Mediator.Send(new UserAvailableRolesQuery() { UserId = query.Id });

        var data = await Mediator.Send(query);

        return Ok(new
        {
            Data = data,
            AvailableRoles = availableRoles
        });
    }

}
