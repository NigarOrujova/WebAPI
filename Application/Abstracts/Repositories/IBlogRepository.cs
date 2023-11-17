using Application.Abstracts.Repositories.Base;
using Domain.Entities;

namespace Application.Abstracts.Repositories;

public interface IBlogRepository:IRepository<Blog> 
{
    Task<object> GetBlogBySlugAsync(string slug);
}
