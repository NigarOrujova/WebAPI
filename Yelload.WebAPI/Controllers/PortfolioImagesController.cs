using Application.PortfolioImages.Commands.CreatePortfolioImage;
using Application.PortfolioImages.Commands.DeletePortfolioImage;
using Application.PortfolioImages.Commands.UpdatePortfolioImage;
using Application.PortfolioImages.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers
{
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
        public async Task<IActionResult> CreateAsync([FromForm] CreatePortfolioImageCommand request)
       => Ok(await Mediator.Send(request));
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdatePortfolioImageCommand request)
       => Ok(await Mediator.Send(request));
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
       => Ok(await Mediator.Send(new DeletePortfolioImageCommand(id)));
    }
}
