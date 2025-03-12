namespace ShopeeFoodClone.WebApi.Stores.Domain.Interfaces;

public interface ILocationRepository : IDisposable
{
    Task<Ward> GetWardByCodeAsync(Expression<Func<Ward, bool>>? filter = null, bool tracked = true);
}
