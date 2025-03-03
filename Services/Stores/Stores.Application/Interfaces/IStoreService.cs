namespace ShopeeFoodClone.WebApi.Stores.Application.Interfaces;

public interface IStoreService
{
    Task<Response> GetAllAsync(int pageSize = 0, int pageNumber = 1);
    Task<Response> GetAsync(Guid storeId);
    Task<Response> CreateAsync(CreateStoreRequest request);
    Task<Response> RemoveAsync(Guid storeId);
    Task<Response> UpdateAsync(StoreDto storeDto);
}