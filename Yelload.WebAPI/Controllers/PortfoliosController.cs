using Application.Abstracts.Common.Exceptions;
using Application.Portfolios.Commands.CreatePortfolio;
using Application.Portfolios.Commands.DeletePortfolio;
using Application.Portfolios.Commands.UpdatePortfolio;
using Application.Portfolios.Queries;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers;

public class PortfoliosController : ApiControllerBase
{
    [HttpGet("{slug}")]
    public async Task<IActionResult> GetByIdAsync(string slug)
    {
        try
        {
            var query = new PortfolioSingleQuery { Slug = slug };
            var portfolio = await Mediator.Send(query);

            if (portfolio == null)
            {
                return NotFound("Portfolio not found");
            }

            return Ok(portfolio);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status502BadGateway, new JsonResponse { Status = "Error", Message = ex.Message });
        }
    }
    [HttpGet("languages/{id}")]
    public async Task<IActionResult> GetLanIdAsync([FromRoute] int id)
    => Ok(await Mediator.Send(new PortfolioLanguageQuery(id)));
    [HttpGet("languages")]
    public async Task<IActionResult> GetLanAllAsync()
    => Ok(await Mediator.Send(new PortfolioLanguageAllQuery()));
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    => Ok(await Mediator.Send(new PortfolioAllQuery()));
    [HttpGet("paginate")]
    public async Task<ActionResult<List<Portfolio>>> GetPortfolios(int page = 1, int size = 10)
    {
        var query = new PortfoliosWithPaginationQuery { Page = page, Size = size };
        var result = await Mediator.Send(query);

        return Ok(result);
    }
    [HttpGet("paginatelan")]
    public async Task<ActionResult<List<Portfolio>>> GetPortfoliosLan(int? categoryId,int page = 1, int size = 10)
    {
        var query = new PortfolioLanguageWithPaginationQuery { CategoryId = categoryId, Page = page, Size = size };
        var result = await Mediator.Send(query);

        return Ok(result);
    }
    [HttpPost]
    [Authorize(Policy = "admin.portfolios.post")]
    public async Task<IActionResult> CreateAsync([FromForm] CreatePortfolioCommand request)
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
    [Authorize(Policy = "admin.portfolios.put")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromForm] Portfolio request)
    {
        try
        {
            var result = await Mediator.Send(new UpdatePortfolioCommand(id, request));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status502BadGateway, new FileException (ex.Message ));
        }
    }
    [HttpDelete("{id}")]
    [Authorize(Policy = "admin.portfolios.delete")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    => Ok(await Mediator.Send(new DeletePortfolioCommand(id)));
}