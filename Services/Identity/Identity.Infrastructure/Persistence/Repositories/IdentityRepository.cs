namespace ShopeeFoodClone.WebApi.Identity.Infrastructure.Persistence.Repositories;

public class IdentityRepository : IIdentityRepository
{
    private readonly IdentityContext _context;
    private readonly DbSet<AppUser> _userDbSet;
    private readonly DbSet<RefreshToken> _refreshTokenDbSet;
    private bool _disposed = false;

    public IdentityRepository(IdentityContext context)
    {
        _context = context;
        _userDbSet = _context.AppUsers;
        _refreshTokenDbSet = _context.RefreshTokens;
    }
    
    public async Task<AppUser> GetUserAsync(Expression<Func<AppUser, bool>>? filter = null, bool tracked = true)
    {
        IQueryable<AppUser> query = _userDbSet;

        if (!tracked)
            query = query.AsNoTracking();

        if (filter is not null)
            query = query.Where(filter);

        AppUser? user = await query.FirstOrDefaultAsync();

        return user!;
    }
    
    public async Task<RefreshToken?> GetRefreshTokenAsync(
        Expression<Func<RefreshToken, bool>>? filter = null, 
        Func<IQueryable<RefreshToken>, IQueryable<RefreshToken>>? include = null,
        bool tracked = true)
    {
        IQueryable<RefreshToken> query = _refreshTokenDbSet;

        if (!tracked)
            query = query.AsNoTracking();
            
        if (include is not null)
            query = include(query);

        if (filter is not null)
            query = query.Where(filter);

        RefreshToken? refreshToken = await query.FirstOrDefaultAsync();

        return refreshToken;
    }

    public async Task CreateRefreshTokenAsync(RefreshToken refreshToken)
    {
        _refreshTokenDbSet.Add(refreshToken);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken)
    {
        var existingToken = await _refreshTokenDbSet.FindAsync(refreshToken.Id);

        if (existingToken != null)
        {
            _context.Entry(existingToken).CurrentValues.SetValues(refreshToken);
        }
        else
        {
            _refreshTokenDbSet.Attach(refreshToken);
            _context.Entry(refreshToken).State = EntityState.Modified;
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