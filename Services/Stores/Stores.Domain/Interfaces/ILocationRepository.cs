namespace ShopeeFoodClone.WebApi.Stores.Domain.Interfaces;

public interface ILocationRepository<TLocation> : IDisposable where TLocation : class
{
    Task<TLocation> GetByCodeAsync(Expression<Func<TLocation, bool>>? filter = null, bool tracked = true);
    Task<IEnumerable<TLocation>> GetAllAsync(Expression<Func<TLocation, bool>>? filter = null, bool tracked = true, int pageSize = 0, int pageNumber = 1);
}
