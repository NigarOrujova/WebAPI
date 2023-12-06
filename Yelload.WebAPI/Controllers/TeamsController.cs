using Application.Abstracts.Common.Exceptions;
using Application.Blogs.Commands.UpdateBlog;
using Application.Portfolios.Commands.UpdatePortfolio;
using Application.Teams.Commands.CreateTeam;
using Application.Teams.Commands.DeleteTeam;
using Application.Teams.Commands.SortTeam;
using Application.Teams.Commands.UpdateTeam;
using Application.Teams.Queries;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers;

public class TeamsController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    => Ok(await Mediator.Send(new TeamSingleQuery(id)));
    [HttpGet("languages/{id}")]
    public async Task<IActionResult> GetLanIdAsync([FromRoute] int id)
    => Ok(await Mediator.Send(new TeamLanguageQuery(id)));
    [HttpGet("paginate")]
    public async Task<ActionResult<List<Team>>> GetTeams(int page = 1, int size = 10)
    {
        var query = new TeamsWithPaginationQuery { Page = page, Size = size };
        var result = await Mediator.Send(query);

        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    => Ok(await Mediator.Send(new TeamAllQuery()));
    [HttpGet("languages")]
    public async Task<IActionResult> GetLanAllAsync()
    => Ok(await Mediator.Send(new TeamLanguageAllQuery()));
    [HttpPost]
    [Authorize(Policy = "admin.teams.post")]
    public async Task<IActionResult> CreateAsync([FromForm] CreateTeamCommand request)
    {
        try
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }
        catch (FileException ex)
        {
            return StatusCode(StatusCodes.Status502BadGateway, new JsonResponse { Status = "Error", Message = ex.Message });
        }
    }
    [HttpPost("teamsort")]
    [Authorize(Policy = "admin.teams.sort")]
    public async Task<IActionResult> SortTeam([FromForm]  TeamSortCommand request)
    {
        try
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status502BadGateway, new JsonResponse { Status = "Error", Message = ex.Message });
        }
    }
    [HttpPut("{id}")]
    [Authorize(Policy = "admin.teams.put")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromForm] Team request)
    {
        try
        {
            var result = await Mediator.Send(new UpdateTeamCommand(id, request));
            return Ok(result);
        }
        catch (FileException ex)
        {
            return StatusCode(StatusCodes.Status502BadGateway, new FileException(ex.Message));
        }
    }
    [HttpDelete("{id}")]
    [Authorize(Policy = "admin.teams.delete")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    => Ok(await Mediator.Send(new DeleteTeamCommand(id)));
}
