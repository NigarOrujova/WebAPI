using System.Linq.Expressions;

namespace Application.Abstracts.Repositories.Base;

public interface IRepository<T>
{
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includes);
    Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includes);
    Task<IEnumerable<T>> GetPaginatedAsync(int page, int pageSize);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<int> GetTotalCountAsync(Expression<Func<T, bool>>? filter = null);
    Task<bool> IsExistAsync(Expression<Func<T, bool>> filter);
}
