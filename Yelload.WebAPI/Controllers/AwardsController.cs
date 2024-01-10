using Application.Awards.Commands.CreateAward;
using Application.Awards.Commands.DeleteAward;
using Application.Awards.Commands.UpdateAward;
using Application.Awards.Queries;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers;

public class AwardsController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    => Ok(await Mediator.Send(new AwardSingleQuery(id)));
    [HttpGet("languages/{id}")]
    public async Task<IActionResult> GetLanIdAsync([FromRoute] int id)
    => Ok(await Mediator.Send(new AwardLanguageQuery(id)));
    [HttpGet("languages")]
    public async Task<IActionResult> GetLanAllAsync()
    => Ok(await Mediator.Send(new AwardLanguageAllQuery()));
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    => Ok(await Mediator.Send(new AwardAllQuery()));
    [HttpPost]
    [Authorize(Policy = "admin.Awards.post")]
    public async Task<IActionResult> CreateAsync([FromForm] CreateAwardCommand request)
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
    [Authorize(Policy = "admin.Awards.put")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromForm] Award request)
    {
        try
        {
            var result = await Mediator.Send(new UpdateAwardCommand(id, request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status502BadGateway, new Exception(ex.Message));
        }
    }
    [HttpDelete("{id}")]
    [Authorize(Policy = "admin.Awards.delete")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    => Ok(await Mediator.Send(new DeteleAwardCommand(id)));
}
