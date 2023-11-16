using Application.Abstracts.Repositories.Base;
using Domain.Entities;

namespace Application.Abstracts.Repositories;

public interface IPortfolioRepository : IRepository<Portfolio> 
{
    Task<object> GetPortfolioBySlugAsync(string slug);
}