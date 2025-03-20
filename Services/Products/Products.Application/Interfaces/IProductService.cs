namespace ShopeeFoodClone.WebApi.Products.Application.Interfaces;

public interface IProductService
{
    Task<Response> GetAllByStoreIdAsync(GetStoresRequest request);
    Task<Response> GetAsync(Guid productId);
    Task<Response> CreateAsync(ProductDto productDto);
    Task<Response> RemoveAsync(Guid productId);
    Task<Response> UpdateAsync(ProductDto productDto);
}
