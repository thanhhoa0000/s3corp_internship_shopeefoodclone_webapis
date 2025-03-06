namespace ShopeeFoodClone.WebApi.Products.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductService(
        IProductRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Get list of products by store ID
    /// </summary>
    /// <param name="storeId">The store ID to get products</param>
    /// <param name="pageSize">Maximum products per page</param>
    /// <param name="pageNumber">Page number to start with</param>
    /// <returns>The products list</returns>
    public async Task<Response> GetAllByStoreIdAsync(Guid storeId, int pageSize = 12, int pageNumber = 1)
    {
        var response = new Response();

        try
        {
            var products = await _repository.GetAllAsync(p => p.StoreId == storeId, tracked: false, pageSize: pageSize, pageNumber: pageNumber);
            
            response.Body = _mapper.Map<IEnumerable<ProductDto>>(products);
            
            return response;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;

            return response;
        }
    }

    /// <summary>
    /// Get a product
    /// </summary>
    /// <param name="productId">The ID of the product</param>
    /// <returns>The product</returns>
    public async Task<Response> GetAsync(Guid productId)
    {
        var response = new Response();

        try
        {
            var product = await _repository.GetAsync(s => s.Id == productId, tracked: false);
            
            response.Body = _mapper.Map<ProductDto>(product);
            
            return response;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            
            return response;
        }
    }

    /// <summary>
    /// Create a product
    /// </summary>
    /// <param name="productDto">The product to create</param>
    /// <returns>The created product</returns>
    public async Task<Response> CreateAsync(ProductDto productDto)
    {
        var response = new Response();

        try
        {
            var product = _mapper.Map<Product>(productDto);
            
            await _repository.CreateAsync(product);
            
            response.Body = _mapper.Map<ProductDto>(product);
            
            return response;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            
            return response;
        }
    }

    /// <summary>
    /// Update the product's metadata
    /// </summary>
    /// <param name="productDto">The product to update</param>
    /// <returns>The updated product</returns>
    public async Task<Response> UpdateAsync(ProductDto productDto)
    {
        var response = new Response();

        try
        {
            var product = await _repository.GetAsync(s => s.Id == productDto.Id, tracked: false);
            
            if (product.ConcurrencyStamp != productDto.ConcurrencyStamp)
            {
                response.IsSuccessful = false;
                response.Message = "Concurrency conflict! Data was modified by another user!";
                
                return response;
            }
            
            var categoryToUpdate = _mapper.Map<Product>(productDto);
            
            await _repository.UpdateAsync(categoryToUpdate);
            
            response.Body = _mapper.Map<ProductDto>(categoryToUpdate);
            
            return response;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            
            return response;
        }
    }

    /// <summary>
    /// Deleted the product
    /// </summary>
    /// <param name="productId">The product's ID to delete</param>
    /// <returns>The deleted product</returns>
    public async Task<Response> RemoveAsync(Guid productId)
    {
        var response = new Response();

        try
        {
            var product = await _repository.GetAsync(s => s.Id == productId, tracked: false);
            
            await _repository.RemoveAsync(product);
            
            response.Body = _mapper.Map<ProductDto>(product);
            
            return response;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            
            return response;
        }
    }
}