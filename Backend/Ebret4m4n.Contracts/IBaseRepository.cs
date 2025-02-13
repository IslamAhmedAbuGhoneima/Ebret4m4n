using System.Linq.Expressions;

namespace Ebret4m4n.Contracts;

public interface IBaseRepository<T> 
{
    IQueryable<T> FindAll(bool trackChanges);

    IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition, bool trackChanges);

    Task<T> FindByIdAsync(Guid id);

    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
}
