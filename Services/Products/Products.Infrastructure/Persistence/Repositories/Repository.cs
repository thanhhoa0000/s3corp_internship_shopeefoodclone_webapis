namespace ShopeeFoodClone.WebApi.Products.Infrastructure.Persistence.Repositories;

public class Repository<T, TContext> : IRepository<T> 
    where T : class, IEntity
    where TContext : DbContext
{
    private readonly TContext _context;
    private readonly DbSet<T> _dbSet;
    private bool _disposed = false;

    public Repository(TContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task CreateAsync(T entity)
    {
        var existingEntity = await _dbSet.FindAsync(entity.Id);
        if (existingEntity == null)
        {
            await _dbSet.AddAsync(entity);
            await SaveAsync();
        }
        else
        {
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        }
    }

    public async Task<T>
        GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
    {
        IQueryable<T> query = _dbSet;

        if (!tracked)
            query = query.AsNoTracking();

        if (filter is not null)
            query = query.Where(filter);

        T? T = await query.FirstOrDefaultAsync();

        return T!;
    }

    public async Task<IEnumerable<T>>
        GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            bool tracked = true,
            int pageSize = 0,
            int pageNumber = 1)
    {
        IQueryable<T> query = _dbSet;

        if (!tracked)
            query = query.AsNoTracking();

        if (filter is not null)
            query = query.Where(filter);

        if (pageSize > 0)
            query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);

        IEnumerable<T> entitiesList = await query.ToListAsync();

        return entitiesList;            
    }

    public async Task UpdateAsync(T entity)
    {
        var existingEntity = await _dbSet.FindAsync(entity.Id);

        if (existingEntity != null)
        {
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        }
        else
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        await SaveAsync();
    }

    public async Task RemoveAsync(T entity)
    {
        var existingEntity = _dbSet.Local.FirstOrDefault(e => e.Id == entity.Id);
        if (existingEntity != null)
        {
            _dbSet.Remove(existingEntity);
        }
        else
        {
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }
        await SaveAsync();

    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }
}