using Application.Teams.Commands.CreateTeam;
using Application.Teams.Commands.DeleteTeam;
using Application.Teams.Commands.UpdateTeam;
using Application.Teams.Queries;
using Application.Medias.Commands.CreateMedia;
using Application.Medias.Commands.DeleteMedia;
using Application.Medias.Commands.UpdateMedia;
using Application.Medias.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers
{
    public class TeamsController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        => Ok(await Mediator.Send(new TeamSingleQuery(id)));
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
       => Ok(await Mediator.Send(new TeamAllQuery()));
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateTeamCommand request)
       => Ok(await Mediator.Send(request));
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateTeamCommand request)
       => Ok(await Mediator.Send(request));
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
       => Ok(await Mediator.Send(new DeleteTeamCommand(id)));
    }
}
