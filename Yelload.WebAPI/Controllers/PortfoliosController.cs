using Application.Portfolios.Commands.CreatePortfolio;
using Application.Portfolios.Commands.DeletePortfolio;
using Application.Portfolios.Commands.UpdatePortfolio;
using Application.Portfolios.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers;

public class PortfoliosController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
   => Ok(await Mediator.Send(new PortfolioSingleQuery(id)));
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
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] CreatePortfolioCommand request)
   => Ok(await Mediator.Send(request));
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromForm] UpdatePortfolioCommand request)
   => Ok(await Mediator.Send(request));
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
   => Ok(await Mediator.Send(new DeletePortfolioCommand(id)));
}