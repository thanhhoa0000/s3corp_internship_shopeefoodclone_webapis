namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Persistence.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly StoreContext _context;
    private readonly DbSet<Ward> _dbSet;
    private bool _disposed = false;

    public LocationRepository(StoreContext context)
    {
        _context = context;
        _dbSet = _context.Set<Ward>();
    }

    public async Task<Ward> GetWardByCodeAsync(Expression<Func<Ward, bool>>? filter = null, bool tracked = true)
    {
        IQueryable<Ward> query = _dbSet;

        if (!tracked)
            query = query.AsNoTracking();

        if (filter is not null)
            query = query.Where(filter);

        Ward? ward = await query.FirstOrDefaultAsync();

        return ward!;
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