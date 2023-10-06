using Application.OurValues.Commands.CreateOurValue;
using Application.OurValues.Commands.DeleteOurValue;
using Application.OurValues.Commands.UpdateOurValue;
using Application.OurValues.Queries;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers
{
    public class OurValuesController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
       => Ok(await Mediator.Send(new OurValueSingleQuery(id)));
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
       => Ok(await Mediator.Send(new OurValueAllQuery()));
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
}
