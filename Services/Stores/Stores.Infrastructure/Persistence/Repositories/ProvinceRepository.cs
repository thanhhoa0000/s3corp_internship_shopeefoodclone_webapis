namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Persistence.Repositories;

public class ProvinceRepository : IProvinceRepository
{
    private readonly StoreContext _context;
    private readonly DbSet<Province> _dbSet;
    private bool _disposed = false;

    public ProvinceRepository(StoreContext context)
    {
        _context = context;
        _dbSet = _context.Set<Province>();
    }
    
    public async Task<IEnumerable<Province>>
        GetAllAsync(
            Expression<Func<Province, bool>>? filter = null,
            bool tracked = true,
            int pageSize = 0,
            int pageNumber = 1)
    {
        IQueryable<Province> query = _dbSet;

        if (!tracked)
            query = query.AsNoTracking();

        if (filter is not null)
            query = query.Where(filter);

        if (pageSize > 0)
            query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);

        IEnumerable<Province> entitiesList = await query.ToListAsync();

        return entitiesList;            
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
