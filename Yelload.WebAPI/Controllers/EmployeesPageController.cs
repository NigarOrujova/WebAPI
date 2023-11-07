using Application.Employees.Commands;
using Application.Employees.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers
{
    public class EmployeesPageController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        => Ok(await Mediator.Send(new EmployeeLanguageQuery()));
        [HttpPut]
        [Authorize(Policy = "admin.about.put")]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateEmployeeCommand request)
        => Ok(await Mediator.Send(request));
    }
}
