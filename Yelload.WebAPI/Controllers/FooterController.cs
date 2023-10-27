﻿using Application.Footers.Commands.UpdateFooter;
using Application.Footers.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers;

public class FooterController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    => Ok(await Mediator.Send(new FooterSingleQuery()));
    [HttpPut]
    [Authorize(Policy = "admin.footer.put")]
    public async Task<IActionResult> UpdateAsync(UpdateFooterCommand request)
    => Ok(await Mediator.Send(request));
}
