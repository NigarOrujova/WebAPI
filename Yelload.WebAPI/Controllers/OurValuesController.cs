using Application.OurValues.Commands.CreateOurValue;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers
{
    public class OurValuesController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateBuildingAsync([FromForm] CreateOurValueCommand request)
       => Ok(await Mediator.Send(request));
    }
}
