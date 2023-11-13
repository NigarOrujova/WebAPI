using Application.Categories.Commands.CreateCategory;
using Application.Categories.Commands.DeleteCategory;
using Application.Categories.Commands.UpdateCategory;
using Application.Categories.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers;

public class CategoriesController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        => Ok(await Mediator.Send(new CategorySingleQuery(id)));
    [HttpGet("languages/{id}")]
    public async Task<IActionResult> GetLanIdAsync([FromRoute] int id)
       => Ok(await Mediator.Send(new CategoryLanguageQuery(id)));
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
        => Ok(await Mediator.Send(new CategoryAllQuery()));
    [HttpGet("languages")]
    public async Task<IActionResult> GetLanAllAsync()
        => Ok(await Mediator.Send(new CategoryLanguageAllQuery()));
    [HttpGet("paginate")]
    public async Task<ActionResult<List<Category>>> GetCategories(int page = 1, int size = 10)
    {
        var query = new CategoriesWithPaginationQuery { Page = page, Size = size };
        var result = await Mediator.Send(query);

        return Ok(result);
    }
    [HttpPost]
    [Authorize(Policy = "admin.categories.post")]
    public async Task<IActionResult> CreateAsync([FromForm] CreateCategoryCommand request)
        => Ok(await Mediator.Send(request));
    [HttpPut("{id}")]
    [Authorize(Policy = "admin.categories.put")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromForm] Category request)
        => Ok(await Mediator.Send(new UpdateCategoryCommand(id,request)));
    [HttpDelete("{id}")]
    [Authorize(Policy = "admin.categories.delete")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        => Ok(await Mediator.Send(new DeleteCategoryCommand(id)));
}
