using System.Linq.Expressions;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task DeleteAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
    Task UpdateAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
}
