namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Persistence.Repositories;

public class LocationRepository<TLocation> : ILocationRepository<TLocation> where TLocation : class
{
    private readonly StoreContext _context;
    private readonly DbSet<TLocation> _dbSet;
    private bool _disposed = false;

    public LocationRepository(StoreContext context)
    {
        _context = context;
        _dbSet = _context.Set<TLocation>();
    }

    public async Task<TLocation> GetByCodeAsync(Expression<Func<TLocation, bool>>? filter = null, bool tracked = true)
    {
        IQueryable<TLocation> query = _dbSet;

        if (!tracked)
            query = query.AsNoTracking();

        if (filter is not null)
            query = query.Where(filter);

        TLocation? location = await query.FirstOrDefaultAsync();

        return location!;
    }
    
    public async Task<IEnumerable<TLocation>>
        GetAllAsync(
            Expression<Func<TLocation, bool>>? filter = null,
            bool tracked = true,
            int pageSize = 0,
            int pageNumber = 1)
    {
        IQueryable<TLocation> query = _dbSet;

        if (!tracked)
            query = query.AsNoTracking();

        if (filter is not null)
            query = query.Where(filter);

        if (pageSize > 0)
            query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);

        IEnumerable<TLocation> locationsList = await query.ToListAsync();

        return locationsList;            
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