namespace ShopeeFoodClone.WebApi.Products.Application.Interfaces;

public interface IProductService
{
    Task<Response> GetAllByStoreIdAsync(GetProductsRequest request);
    Task<Response> GetAsync(Guid productId);
    Task<Response> CreateAsync(CreateProductRequest request);
    Task<Response> RemoveAsync(Guid productId);
    Task<Response> VendorDeleteAsync(Guid productId);
    Task<Response> VendorUpdateAsync(VendorUpdateProductRequest request);
    Task<Response> VendorChangeProductStateAsync(VendorUpdateProductStateRequest request);
}
