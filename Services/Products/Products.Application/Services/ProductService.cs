using Microsoft.EntityFrameworkCore;

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
    /// Get a list of products by store ID
    /// </summary>
    /// <param name="request">The store ID to get products</param>
    /// <returns>The products' list</returns>
    public async Task<Response> GetAllByStoreIdAsync(GetProductsRequest request)
    {
        var response = new Response();

        try
        {
            var storeId = request.StoreId;
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;
            
            Expression<Func<Product, bool>> filter = p => p.StoreId == storeId;

            Func<IQueryable<Product>, IQueryable<Product>> include = query =>
                query.Include(p => p.Menus.Where(i => i.State != MenuState.Inactive));
            
            var products = 
                await _repository
                    .GetAllAsync(filter: filter, include: include, tracked: false, pageSize: pageSize, pageNumber: pageNumber);
            
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
    /// <param name="request">The product to create</param>
    /// <returns>The created product</returns>
    public async Task<Response> CreateAsync(CreateProductRequest request)
    {
        var response = new Response();

        try
        {
            if (await _repository.GetAsync(p => p.Id == request.Id) is not null)
            {
                response.IsSuccessful = false;
                response.Message = "Product already exists!";
                
                return response;
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                StoreId = request.StoreId,
                Name = request.Name,
                Description = request.Description,
                AvailableStock = request.AvailableStock,
                Price = request.Price,
            };
            
            product.CoverImagePath = $"/stores/{product.StoreId}/products/{product.Id}/cover-img.jpg";
            product.ConcurrencyStamp = Guid.NewGuid();
            
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
    /// <param name="request">The product to update</param>
    /// <returns>The updated product</returns>
    public async Task<Response> VendorUpdateAsync(VendorUpdateProductRequest request)
    {
        var response = new Response();

        try
        {
            var product = await _repository.GetAsync(s => s.Id == request.Id, tracked: false);

            if (product is null)
            {
                response.IsSuccessful = false;
                response.Message = "Product not found!";
                
                return response;
            }
            
            if (product.ConcurrencyStamp != request.ConcurrencyStamp)
            {
                response.IsSuccessful = false;
                response.Message = "Concurrency conflict! Data was modified by another user!";
                
                return response;
            }
            
            var categoryToUpdate = _mapper.Map<Product>(request);
            
            categoryToUpdate.LastUpdatedAt = DateTime.UtcNow;
            
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
    /// Update the product's state
    /// </summary>
    /// <param name="request">The product's state to update</param>
    /// <returns>The updated product</returns>
    public async Task<Response> VendorChangeProductStateAsync(VendorUpdateProductStateRequest request)
    {
        var response = new Response();

        try
        {
            var product = await _repository.GetAsync(s => s.Id == request.ProductId, tracked: false);

            if (product is null)
            {
                response.IsSuccessful = false;
                response.Message = "Product not found!";
                
                return response;
            }
            
            if (product.ConcurrencyStamp != request.ConcurrencyStamp)
            {
                response.IsSuccessful = false;
                response.Message = "Concurrency conflict! Data was modified by another user!";
                
                return response;
            }
            
            var productToUpdate = _mapper.Map<ProductDto>(product);
            
            productToUpdate.LastUpdatedAt = DateTime.UtcNow;
            productToUpdate.State = request.State;
            
            await _repository.UpdateAsync(_mapper.Map<Product>(productToUpdate));

            response.Body = productToUpdate;
            
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
    /// Deleted the product (soft delete)
    /// </summary>
    /// <param name="productId">The product's ID to delete</param>
    /// <returns>The deleted product</returns>
    public async Task<Response> VendorDeleteAsync(Guid productId)
    {
        var response = new Response();

        try
        {
            var product = await _repository.GetAsync(s => s.Id == productId, tracked: false);
            
            if (product is null)
            {
                response.IsSuccessful = false;
                response.Message = "Product not found!";
                
                return response;
            }
            
            product.LastUpdatedAt = DateTime.UtcNow;
            product.State = ProductState.Deleted;
            
            await _repository.UpdateAsync(product);
            
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
            
            if (product is null)
            {
                response.IsSuccessful = false;
                response.Message = "Product not found!";
                
                return response;
            }
            
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