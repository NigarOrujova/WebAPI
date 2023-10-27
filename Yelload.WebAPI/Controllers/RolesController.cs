using Infrastructure.Identity.Roles.Commands;
using Infrastructure.Identity.Roles.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers;

public class RolesController : ApiControllerBase
{
    [HttpGet("{id}")]
    [Authorize(Policy = "admin.teams.get")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
   => Ok(await Mediator.Send(new RoleSingleQuery(id)));
    [HttpPost("create")]
    [Authorize(Policy = "admin.teams.create")]
    public async Task<IActionResult> CreateAsync(RoleCreateCommand request)
   => Ok(await Mediator.Send(request));
    [HttpPost("setprincipal")]
    [Authorize(Policy = "admin.teams.setprincipal")]
    public async Task<IActionResult> SetPrincipalAsync(RoleSetPrincipalCommand request)
   => Ok(await Mediator.Send(request));
    [HttpPost("rolesorting")]
    [Authorize(Roles = "sa")]
    public async Task<IActionResult> RoleSorting(RoleSortCommand request)
  => Ok(await Mediator.Send(request));
}
