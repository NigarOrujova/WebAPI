using Application.Counters.Commands.CreateCounter;
using Application.Counters.Commands.DeleteCounter;
using Application.Counters.Commands.UpdateCounter;
using Application.Counters.Queries;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers
{
    public class CountersController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        => Ok(await Mediator.Send(new CounterSingleQuery(id)));
        [HttpGet("languages/{id}")]
        public async Task<IActionResult> GetLanIdAsync([FromRoute] int id)
        => Ok(await Mediator.Send(new CounterLanguageQuery(id)));
        [HttpGet("languages")]
        public async Task<IActionResult> GetLanAllAsync()
        => Ok(await Mediator.Send(new CounterLanguageAllQuery()));
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        => Ok(await Mediator.Send(new CounterAllQuery()));
        [HttpPost]
        [Authorize(Policy = "admin.Counters.post")]
        public async Task<IActionResult> CreateAsync([FromForm] CreateCounterCommand request)
        {
            try
            {
                var result = await Mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new JsonResponse { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPut("{id}")]
        [Authorize(Policy = "admin.Counters.put")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromForm] Counter request)
        {
            try
            {
                var result = await Mediator.Send(new UpdateCounterCommand(id, request));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new Exception(ex.Message));
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Policy = "admin.Counters.delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        => Ok(await Mediator.Send(new DeteleCounterCommand(id)));
    }
}
