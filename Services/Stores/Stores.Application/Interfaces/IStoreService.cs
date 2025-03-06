namespace ShopeeFoodClone.WebApi.Stores.Application.Interfaces;

public interface IStoreService
{
    Task<Response> GetByLocationAsync(GetStoreByLocationRequest request, int pageSize = 0, int pageNumber = 1);
    Task<Response> GetByLocationAndCategoryAsync(GetStoreRequest request, int pageSize = 0, int pageNumber = 1);
    Task<Response> GetAllByUserIdAsync(Guid userId, int pageSize = 0, int pageNumber = 1);
    Task<Response> GetAsync(Guid storeId);
    Task<Response> CreateAsync(CreateStoreRequest request);
    Task<Response> RemoveAsync(Guid storeId);
    Task<Response> UpdateAsync(StoreDto storeDto);
}