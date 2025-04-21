namespace ShopeeFoodClone.WebApi.Stores.Application.Interfaces;

public interface IStoreService
{
    Task<Response> GetByLocationAndCategoryAsync(GetStoresRequest request);
    Task<Response> GetAllByVendorIdAsync(GetStoresByVendorIdRequest request);
    Task<Response> GetAsync(Guid storeId);
    Task<Response> GetNameAsync(Guid storeId);
    Task<Response> CreateAsync(CreateStoreRequest request);
    Task<Response> VendorDeleteAsync(Guid storeId);
    Task<Response> RemoveAsync(Guid storeId);
    Task<Response> VendorUpdateAsync(VendorUpdateStoreRequest request);
    Task<Response> AdminUpdateAsync(AdminUpdateStoreRequest request);
    Response GetStoresCount(GetStoresCountRequest request);
}
