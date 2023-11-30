using Application.Abstracts.Common.Exceptions;
using Application.Employees.Commands;
using Application.Employees.Queries;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers
{
    public class EmployeesPageController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        => Ok(await Mediator.Send(new EmployeeSingleQuery()));
        [HttpGet("languages")]
        public async Task<IActionResult> GetLanAsync()
        => Ok(await Mediator.Send(new EmployeeLanguageQuery()));
        [HttpPut]
        [Authorize(Policy = "admin.about.put")]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateEmployeeCommand request)
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
    }
}
