namespace ShopeeFoodClone.WebApi.Products.Application.Interfaces;

public interface IProductService
{
    Task<Response> GetAllAsync(int pageSize = 0, int pageNumber = 1);
    Task<Response> GetAllByStoreIdAsync(Guid storeId, int pageSize = 0, int pageNumber = 1);
    Task<Response> GetAsync(Guid productId);
    Task<Response> CreateAsync(ProductDto productDto);
    Task<Response> RemoveAsync(Guid productId);
    Task<Response> UpdateAsync(ProductDto productDto);
    Task HandleProductRequest(ProductInfoRequest request);
}
