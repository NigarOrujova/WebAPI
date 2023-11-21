using Application.Blogs.Commands.CreateBlog;
using Application.Blogs.Commands.DeleteBlog;
using Application.Blogs.Commands.UpdateBlog;
using Application.Blogs.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yelload.WebAPI.Controllers.Base;

namespace Yelload.WebAPI.Controllers;

public class BlogController : ApiControllerBase
{
    [HttpGet("{slug}")]
    public async Task<IActionResult> GetByIdAsync(string slug)
    {
        var query = new BlogSingleQuery { Slug = slug };
        var blog = await Mediator.Send(query);

        if (blog == null)
        {
            return NotFound("Blog not found");
        }

        return Ok(blog);
    }
    [HttpGet("languages/{id}")]
    public async Task<IActionResult> GetLanIdAsync([FromRoute] int id)
    => Ok(await Mediator.Send(new BlogLanguageQuery(id)));
    [HttpGet("languages")]
    public async Task<IActionResult> GetLanAllAsync()
    => Ok(await Mediator.Send(new BlogLanguageAllQuery()));
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    => Ok(await Mediator.Send(new BlogAllQuery()));
    [HttpGet("paginate")]
    public async Task<ActionResult<List<Blog>>> GetBlogs(int page = 1, int size = 10)
    {
        var query = new BlogsWithPaginationQuery { Page = page, Size = size };
        var result = await Mediator.Send(query);

        return Ok(result);
    }
    [HttpGet("paginatelan")]
    public async Task<ActionResult<List<Blog>>> GetBlogsLan(int? tagId, int page = 1, int size = 10)
    {
        var query = new BlogLanguageWithPaginationQuery { TagId = tagId, Page = page, Size = size };
        var result = await Mediator.Send(query);

        return Ok(result);
    }
    [HttpPost]
    [Authorize(Policy = "admin.Blogs.post")]
    public async Task<IActionResult> CreateAsync([FromForm] CreateBlogCommand request)
    => Ok(await Mediator.Send(request));
    [HttpPut("{id}")]
    [Authorize(Policy = "admin.Blogs.put")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromForm] Blog request)
    => Ok(await Mediator.Send(new UpdateBlogCommand(id, request)));
    [HttpPut("publish/{id}")]
    [Authorize(Policy = "admin.Blogs.publish")]
    public async Task<IActionResult> BlogPublish([FromRoute] int id)
    => Ok(await Mediator.Send(new BlogPublishQuery(id)));
    [HttpDelete("{id}")]
    [Authorize(Policy = "admin.Blogs.delete")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    => Ok(await Mediator.Send(new DeteleBlogCommand(id)));
}
