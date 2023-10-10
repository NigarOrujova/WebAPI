using Application.OurValues.Commands.CreateOurValue;
using Application.OurValues.Commands.DeleteOurValue;
using Application.OurValues.Commands.UpdateOurValue;
using Application.OurValues.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers;

public class OurValuesController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
   => Ok(await Mediator.Send(new OurValueSingleQuery(id)));
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
   => Ok(await Mediator.Send(new OurValueAllQuery()));
    [HttpGet("paginate")]
    public async Task<ActionResult<List<OurValue>>> GetOurValues(int page = 1, int size = 10)
    {
        var query = new OurValuesWithPaginationQuery { Page = page, Size = size };
        var result = await Mediator.Send(query);

        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] CreateOurValueCommand request)
   => Ok(await Mediator.Send(request));
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromForm] UpdateOurValueCommand request)
   => Ok(await Mediator.Send(request));
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
   => Ok(await Mediator.Send(new DeleteOurValueCommand(id)));
}
