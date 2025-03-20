namespace ShopeeFoodClone.WebApi.Stores.Application.Interfaces;

public interface IStoreService
{
    Task<Response> GetByLocationAndCategoryAsync(GetStoresRequest request);
    Task<Response> GetAllByVendorIdAsync(GetStoresByVendorIdRequest request);
    Task<Response> GetAsync(Guid storeId);
    Task<Response> CreateAsync(CreateStoreRequest request);
    Task<Response> RemoveAsync(Guid storeId);
    Task<Response> UpdateAsync(StoreDto storeDto);
}
