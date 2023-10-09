using Application.Categories.Commands.CreateCategory;
using Application.Categories.Commands.DeleteCategory;
using Application.Categories.Commands.UpdateCategory;
using Application.Categories.Queries;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers;

public class CategoriesController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
 => Ok(await Mediator.Send(new CategorySingleQuery(id)));
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
   => Ok(await Mediator.Send(new CategoryAllQuery()));
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] CreateCategoryCommand request)
   => Ok(await Mediator.Send(request));
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromForm] UpdateCategoryCommand request)
   => Ok(await Mediator.Send(request));
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
   => Ok(await Mediator.Send(new DeleteCategoryCommand(id)));
}
