using Application.Headers.Commands.CreateHeader;
using Application.Headers.Commands.DeleteHeader;
using Application.Headers.Commands.UpdateHeader;
using Application.Headers.Queries;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers;

public class HeaderController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    => Ok(await Mediator.Send(new HeaderSingleQuery(id)));
    [HttpGet("languages/{id}")]
    public async Task<IActionResult> GetLanIdAsync([FromRoute] int id)
    => Ok(await Mediator.Send(new HeaderLanguageQuery(id)));
    [HttpGet("languages")]
    public async Task<IActionResult> GetLanAllAsync()
    => Ok(await Mediator.Send(new HeaderLanguageAllQuery()));
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    => Ok(await Mediator.Send(new HeaderAllQuery()));
    [HttpPost]
    [Authorize(Policy = "admin.headers.post")]
    public async Task<IActionResult> CreateAsync([FromForm] CreateHeaderCommand request)
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
    [Authorize(Policy = "admin.headers.put")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromForm] Header request)
    {
        try
        {
            var result = await Mediator.Send(new UpdateHeaderCommand(id, request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status502BadGateway, new Exception(ex.Message));
        }
    }
    [HttpDelete("{id}")]
    [Authorize(Policy = "admin.headers.delete")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    => Ok(await Mediator.Send(new DeleteHeaderCommand(id)));
}
