using Ebret4m4n.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ebret4m4n.Repository.Repositories;

public class BaseRepository<T>(EbretAmanDbContext context) :
    IBaseRepository<T> where T : class
{

    public void Add(T entity)
        => context.Add(entity);


    public IQueryable<T> FindAll(bool trackChanges)
        => context.Set<T>();


    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition, bool trackChanges)
        => trackChanges ?
        context.Set<T>().Where(condition) :
        context.Set<T>().AsNoTracking().Where(condition);

    public async Task<T> FindByIdAsync(Guid id)
       => await context.Set<T>().FindAsync(id);
        

    public void Remove(T entity)
        => context.Remove(entity);

    public void Update(T entity)
        => context.Update(entity);
}
