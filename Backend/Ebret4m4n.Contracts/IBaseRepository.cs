using System.Linq.Expressions;

namespace Ebret4m4n.Contracts;

public interface IBaseRepository<T> 
{
    IQueryable<T> FindAll(bool trackChanges, params string[]? includes);

    IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition, bool trackChanges, params string[]? includes);

    Task<T> FindAsync(Expression<Func<T, bool>> condition, bool trackChanges, params string[]? includes);

    Task<bool> ExistsAsync(Expression<Func<T, bool>> condition);

    Task AddAsync(T entity);

    Task AddRangeAsync(IEnumerable<T> entities);

    void Update(T entity);

    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entities);
}
