using Application.Abstracts.Common.Exceptions;
using Application.Medias.Commands.CreateMedia;
using Application.Medias.Commands.DeleteMedia;
using Application.Medias.Commands.UpdateMedia;
using Application.Medias.Queries;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers;

public class MediasController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
   => Ok(await Mediator.Send(new MediaSingleQuery(id)));
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
   => Ok(await Mediator.Send(new MediaAllQuery()));
    [HttpGet("paginate")]
    public async Task<ActionResult<List<Media>>> GetMedias(int page = 1, int size = 10)
    {
        var query = new MediasWithPaginationQuery { Page = page, Size = size };
        var result = await Mediator.Send(query);

        return Ok(result);
    }
    [HttpPost]
    [Authorize(Policy = "admin.medias.post")]
    public async Task<IActionResult> CreateAsync([FromForm] CreateMediaCommand request)
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
    [HttpPut("{id}")]
    [Authorize(Policy = "admin.medias.put")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromForm] Media request)
    {
        try
        {
            var result = await Mediator.Send(new UpdateMediaCommand(id, request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status502BadGateway, new Exception(ex.Message));
        }
    }
    [HttpDelete("{id}")]
    [Authorize(Policy = "admin.medias.delete")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
   => Ok(await Mediator.Send(new DeleteMediaCommand(id)));
}
