using Ebret4m4n.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ebret4m4n.Repository.Repositories;

public class BaseRepository<T>(EbretAmanDbContext context) :
    IBaseRepository<T> where T : class
{

    public async Task AddAsync(T entity)
        => await context.AddAsync(entity);

    public async Task AddRangeAsync(IEnumerable<T> entities)
        => await context.AddRangeAsync(entities);

    public IQueryable<T> FindAll(bool trackChanges, params string[]? includes)
    {
        IQueryable<T> query = trackChanges ? context.Set<T>()
            : context.Set<T>().AsNoTracking();

        if (includes is not null)
        {
            foreach (string include in includes)
                query = query.Include(include);
        }

        return query;
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition, bool trackChanges, params string[]? includes)
    {
        IQueryable<T> query = trackChanges ? context.Set<T>()
            : context.Set<T>().AsNoTracking();

        if (includes is not null)
        {
            foreach (string include in includes)
                query = query.Include(include);
        }

        return query.Where(condition);
    }

    public async Task<T> FindAsync(Expression<Func<T, bool>> condition, bool trackChanges, params string[]? includes)
    {
        IQueryable<T> entity = trackChanges ? context.Set<T>() :
            context.Set<T>().AsNoTracking();
        
        if(includes is not null)
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

    public void RemoveRange(IEnumerable<T> entities)
        => context.RemoveRange(entities);

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> condition)
        => await context.Set<T>().AnyAsync(condition);
}
