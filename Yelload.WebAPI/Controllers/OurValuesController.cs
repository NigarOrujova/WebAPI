﻿using Application.Abstracts.Common.Exceptions;
using Application.Medias.Commands.UpdateMedia;
using Application.OurValues.Commands.CreateOurValue;
using Application.OurValues.Commands.DeleteOurValue;
using Application.OurValues.Commands.UpdateOurValue;
using Application.OurValues.Queries;
using Application.PortfolioImages.Commands.UpdatePortfolioImage;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers;

public class OurValuesController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
     => Ok(await Mediator.Send(new OurValueSingleQuery(id)));
    [HttpGet("languages/{id}")]
    public async Task<IActionResult> GetLanIdAsync([FromRoute] int id)
     => Ok(await Mediator.Send(new OurValueLanguageQuery(id)));
    [HttpGet("languages")]
    public async Task<IActionResult> GetLanAllAsync()
    => Ok(await Mediator.Send(new OurValueLanguageAllQuery()));
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
    [Authorize(Policy = "admin.ourvalues.post")]
    public async Task<IActionResult> CreateAsync([FromForm] CreateOurValueCommand request)
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
    [Authorize(Policy = "admin.ourvalues.put")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromForm] OurValue request)
    {
        try
        {
            var result = await Mediator.Send(new UpdateOurValueCommand(id, request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status502BadGateway, new Exception(ex.Message));
        }
    }
    [HttpDelete("{id}")]
    [Authorize(Policy = "admin.ourvalues.delete")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
   => Ok(await Mediator.Send(new DeleteOurValueCommand(id)));
}
