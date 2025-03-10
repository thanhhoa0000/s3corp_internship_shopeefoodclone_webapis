namespace ShopeeFoodClone.WebApi.Stores.Domain.Interfaces;

public interface IProvinceRepository : IDisposable
{
    Task<IEnumerable<Province>> GetAllAsync(Expression<Func<Province, bool>>? filter = null, bool tracked = true, int pageSize = 0, int pageNumber = 1);
}
