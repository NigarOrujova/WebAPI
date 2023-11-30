using Application.Portfolios.Commands.UpdatePortfolio;
using Application.Tags.Commands.CreateTag;
using Application.Tags.Commands.DeleteTag;
using Application.Tags.Commands.UpdateTag;
using Application.Tags.Queries;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers;

public class TagsController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
     => Ok(await Mediator.Send(new TagSingleQuery(id)));
    [HttpGet("languages/{id}")]
    public async Task<IActionResult> GetLanIdAsync([FromRoute] int id)
    => Ok(await Mediator.Send(new TagLanguageQuery(id)));
    [HttpGet("languages")]
    public async Task<IActionResult> GetLanAllAsync()
    => Ok(await Mediator.Send(new TagLanguageAllQuery()));
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    => Ok(await Mediator.Send(new TagAllQuery()));
    //[HttpGet("paginate")]
    //public async Task<ActionResult<List<Tag>>> GetTags(int page = 1, int size = 10)
    //{
    //    var query = new TeamsWithPaginationQuery { Page = page, Size = size };
    //    var result = await Mediator.Send(query);

    //    return Ok(result);
    //}
    [HttpPost]
    [Authorize(Policy = "admin.Tags.post")]
    public async Task<IActionResult> CreateAsync([FromForm] CreateTagCommand request)
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
    [Authorize(Policy = "admin.Tags.put")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromForm] Tag request)
    {
        try
        {
            var result = await Mediator.Send(new UpdateTagCommand(id, request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status502BadGateway, new Exception(ex.Message ));
        }
    }
    [HttpDelete("{id}")]
    [Authorize(Policy = "admin.Tags.delete")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    => Ok(await Mediator.Send(new DeteleTagCommand(id)));
}
