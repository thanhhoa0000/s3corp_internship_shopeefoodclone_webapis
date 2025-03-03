namespace ShopeeFoodClone.WebApi.Stores.Application.Services;

public class StoreService : IStoreService
{
    private readonly IStoreRepository _repository;
    private readonly HttpContext _httpContext;
    private readonly IMapper _mapper;

    public StoreService(
        IStoreRepository repository,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper)
    {
        _repository = repository;
        _httpContext = httpContextAccessor.HttpContext!;
        _mapper = mapper;
    }

    /// <summary>
    /// Get list of stores
    /// </summary>
    /// <param name="pageSize">Pages number to get roles</param>
    /// <param name="pageNumber">Page number to start with</param>
    /// <returns>The stores list</returns>
    public async Task<Response> GetAllAsync(int pageSize = 0, int pageNumber = 1)
    {
        var response = new Response();

        try
        {
            var stores = await _repository.GetAllAsync(tracked: false, pageSize: pageSize, pageNumber: pageNumber);
            
            var pagination = new Pagination()
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
            };
            
            _httpContext.Response.Headers["X-Pagination"] = JsonSerializer.Serialize(pagination);
            
            response.Body = _mapper.Map<IEnumerable<StoreDto>>(stores);
            
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
    /// Get a store
    /// </summary>
    /// <param name="storeId">The ID of the store</param>
    /// <returns>The store</returns>
    public async Task<Response> GetAsync(Guid storeId)
    {
        var response = new Response();

        try
        {
            var store = await _repository.GetAsync(s => s.Id == storeId, tracked: false);
            
            response.Body = _mapper.Map<StoreDto>(store);
            
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
    /// Create a Store
    /// </summary>
    /// <param name="request">The Store to create and the needed information</param>
    /// <returns>The created store</returns>
    public async Task<Response> CreateAsync(CreateStoreRequest request)
    {
        var response = new Response();

        try
        {
            var store = _mapper.Map<Store>(request.Store);
            var categories = _mapper.Map<ICollection<Category>>(request.Categories);

            foreach (var category in categories)
            {
                store.Categories.Add(category);
            }
            
            await _repository.CreateAsync(store);
            
            response.Body = _mapper.Map<StoreDto>(store);
            
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
    /// Update the store's metadata
    /// </summary>
    /// <param name="storeDto">The store to update</param>
    /// <returns>The updated store</returns>
    public async Task<Response> UpdateAsync(StoreDto storeDto)
    {
        var response = new Response();

        try
        {
            var store = _mapper.Map<Store>(storeDto);
            
            await _repository.UpdateAsync(store);
            
            response.Body = _mapper.Map<StoreDto>(store);
            
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
    /// Deleted the store
    /// </summary>
    /// <param name="storeId">The store'ID to delete</param>
    /// <returns>The deleted store</returns>
    public async Task<Response> RemoveAsync(Guid storeId)
    {
        var response = new Response();

        try
        {
            var store = await _repository.GetAsync(s => s.Id == storeId, tracked: false);
            
            await _repository.RemoveAsync(store);
            
            response.Body = _mapper.Map<StoreDto>(store);
            
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