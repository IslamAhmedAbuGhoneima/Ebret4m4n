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
        => trackChanges ? context.Set<T>() :
        context.Set<T>().AsNoTracking();


    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition, bool trackChanges)
        => trackChanges ?
        context.Set<T>().Where(condition) :
        context.Set<T>().AsNoTracking().Where(condition);

    public async Task<T> FindAsync(Expression<Func<T, bool>> condition, bool trackChanges, params string[]? includes)
    {
        IQueryable<T> entity = trackChanges ? context.Set<T>() :
            context.Set<T>().AsNoTracking();

        if (includes is not null)
        {
            foreach (string include in includes)
                entity = entity.Include(include);
        }
        return await entity.FirstOrDefaultAsync(condition);
    }
    public void Remove(T entity)
        => context.Remove(entity);

    public void Update(T entity)
        => context.Update(entity);
}
