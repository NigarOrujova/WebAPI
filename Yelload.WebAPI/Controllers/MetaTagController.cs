using Application.MetaDatas.Contact.Commands;
using Application.MetaDatas.Contact.Queries;
using Application.MetaDatas.Contacts.Queries;
using Application.MetaDatas.Esc.Commands;
using Application.MetaDatas.Esc.Queries;
using Application.MetaDatas.Home.Commands;
using Application.MetaDatas.Home.Queries;
using Application.MetaDatas.Love.Commands;
using Application.MetaDatas.Love.Queries;
using Application.MetaDatas.Wes.Commands;
using Application.MetaDatas.Wes.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;
namespace Yelload.WebAPI.Controllers;
public class MetaTagController : ApiControllerBase
{
    [HttpGet("home")]
    public async Task<IActionResult> GetHomeAsync()
    => Ok(await Mediator.Send(new HomeSingleQuery()));
    [HttpGet("home/languages")]
    public async Task<IActionResult> GetLanAsync()
    => Ok(await Mediator.Send(new HomeLanguageQuery()));
    [HttpPut("home")]
    [Authorize(Policy = "admin.home.put")]
    public async Task<IActionResult> UpdateAsync([FromForm] HomeUpdateCommand request)
    => Ok(await Mediator.Send(request));
    [HttpGet("esc")]
    public async Task<IActionResult> GetEscAsync()
    => Ok(await Mediator.Send(new EscSingleQuery()));
    [HttpGet("esc/languages")]
    public async Task<IActionResult> GetLanEscAsync()
    => Ok(await Mediator.Send(new EscLanguageQuery()));
    [HttpPut("esc")]
    [Authorize(Policy = "admin.esc.put")]
    public async Task<IActionResult> UpdateAsync([FromForm] EscUpdateCommand request)
    => Ok(await Mediator.Send(request));
    [HttpGet("contact")]
    public async Task<IActionResult> GetContactAsync()
    => Ok(await Mediator.Send(new ContactSingleQuery()));
    [HttpGet("contact/languages")]
    public async Task<IActionResult> GetLanContactAsync()
    => Ok(await Mediator.Send(new ContactLanguageQuery()));
    [HttpPut("contact")]
    [Authorize(Policy = "admin.contact.put")]
    public async Task<IActionResult> UpdateAsync([FromForm] ContactUpdateCommand request)
    => Ok(await Mediator.Send(request));
    [HttpGet("love")]
    public async Task<IActionResult> GetLoveAsync()
    => Ok(await Mediator.Send(new LoveSingleQuery()));
    [HttpGet("love/languages")]
    public async Task<IActionResult> GetLanLoveAsync()
    => Ok(await Mediator.Send(new LoveLanguageQuery()));
    [HttpPut("love")]
    [Authorize(Policy = "admin.love.put")]
    public async Task<IActionResult> UpdateAsync([FromForm] LoveUpdateCommand request)
    => Ok(await Mediator.Send(request));
    [HttpGet("we")]
    public async Task<IActionResult> GetWeAsync()
    => Ok(await Mediator.Send(new WeSingleQuery()));
    [HttpGet("we/languages")]
    public async Task<IActionResult> GetLanWeAsync()
    => Ok(await Mediator.Send(new WeLanguageQuery()));
    [HttpPut("we")]
    [Authorize(Policy = "admin.we.put")]
    public async Task<IActionResult> UpdateAsync([FromForm] WeUpdateCommand request)
    {
        try
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
