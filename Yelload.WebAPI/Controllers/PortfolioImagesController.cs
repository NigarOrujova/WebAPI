using Application.Abstracts.Common.Exceptions;
using Application.OurValues.Commands.UpdateOurValue;
using Application.PortfolioImages.Commands.CreatePortfolioImage;
using Application.PortfolioImages.Commands.DeletePortfolioImage;
using Application.PortfolioImages.Commands.UpdatePortfolioImage;
using Application.PortfolioImages.Queries;
using Application.Portfolios.Commands.UpdatePortfolio;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers;

public class PortfolioImagesController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    => Ok(await Mediator.Send(new PortfolioImageSingleQuery(id)));
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    => Ok(await Mediator.Send(new PortfolioImageAllQuery()));
    [HttpGet("paginate")]
    public async Task<ActionResult<List<PortfolioImage>>> GetPortfolioImages(int page = 1, int size = 10)
    {
        var query = new PortfolioImagesWithPaginationQuery { Page = page, Size = size };
        var result = await Mediator.Send(query);

        return Ok(result);
    }
    [HttpPost]
    [Authorize(Policy = "admin.portfolioimages.post")]
    public async Task<IActionResult> CreateAsync([FromForm] CreatePortfolioImageCommand request)
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
    [Authorize(Policy = "admin.portfolioimages.put")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromForm] PortfolioImage request)
    {
        try
        {
            var result = await Mediator.Send(new UpdatePortfolioImageCommand(id, request));
            return Ok(result);
        }
        catch (FileException ex)
        {
            return StatusCode(StatusCodes.Status502BadGateway, new FileException(ex.Message ));
        }
    }
    [HttpDelete("{id}")]
    [Authorize(Policy = "admin.portfolioimages.delete")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
   => Ok(await Mediator.Send(new DeletePortfolioImageCommand(id)));
}
