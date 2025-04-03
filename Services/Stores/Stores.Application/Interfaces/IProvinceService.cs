namespace ShopeeFoodClone.WebApi.Stores.Application.Interfaces;

public interface IProvinceService
{
    Task<Response> GetAllAsync(int pageSize = 0, int pageNumber = 1);
    Task<Response> GetNamesWithStoresCountAsync(int pageSize = 0, int pageNumber = 1);
}
