using Application.Medias.Commands.CreateMedia;
using Application.Medias.Commands.DeleteMedia;
using Application.Medias.Commands.UpdateMedia;
using Application.Medias.Queries;
using Domain.Entities;
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
    public async Task<IActionResult> CreateAsync([FromForm] CreateMediaCommand request)
   => Ok(await Mediator.Send(request));
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromForm] UpdateMediaCommand request)
   => Ok(await Mediator.Send(request));
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
   => Ok(await Mediator.Send(new DeleteMediaCommand(id)));
}
