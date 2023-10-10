using Infrastructure.Identity.Roles.Commands;
using Infrastructure.Identity.Roles.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers;

public class RolesController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
   => Ok(await Mediator.Send(new RoleSingleQuery(id)));
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync([FromForm] RoleCreateCommand request)
   => Ok(await Mediator.Send(request));
    [HttpPost("setprincipal")]
    public async Task<IActionResult> SetPrincipalAsync([FromForm] RoleSetPrincipalCommand request)
   => Ok(await Mediator.Send(request));
    [HttpPost("rolesorting")]
    [Authorize(Roles = "sa")]
    public async Task<IActionResult> RoleSorting([FromForm] RoleSortCommand request)
  => Ok(await Mediator.Send(request));
}
