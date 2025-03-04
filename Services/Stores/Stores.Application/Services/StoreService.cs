namespace ShopeeFoodClone.WebApi.Stores.Application.Services;

public class StoreService : IStoreService
{
    private readonly IStoreRepository _storeRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly HttpContext _httpContext;
    private readonly IMapper _mapper;

    public StoreService(
        IStoreRepository repository,
        ICategoryRepository categoryRepository,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper)
    {
        _storeRepository = repository;
        _categoryRepository = categoryRepository;
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
            var stores = await _storeRepository.GetAllAsync(tracked: false, pageSize: pageSize, pageNumber: pageNumber);
            
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
    /// Get stores list by owner's userId
    /// </summary>
    /// <param name="userId">The owner's userId</param>
    /// <param name="pageSize">Pages number to get roles</param>
    /// <param name="pageNumber">Page number to start with</param>
    /// <returns>The stores list</returns>
    public async Task<Response> GetAllByUserIdAsync(Guid userId, int pageSize = 0, int pageNumber = 1)
    {
        var response = new Response();

        try
        {
            var stores = await _storeRepository.GetAllAsync(s => s.UserId == userId, tracked: false, pageSize: pageSize, pageNumber: pageNumber);
            
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
            var store = await _storeRepository.GetAsync(s => s.Id == storeId, tracked: false);
            
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
            var cateIds = request.Categories.Select(c => c.Id).ToList();
            
            var existingCategories = (await _categoryRepository.GetAllAsync(c => cateIds.Contains(c.Id))).ToList();

            if (existingCategories.Count != cateIds.Count)
            {
                response.IsSuccessful = false;
                response.Message = "One or more categories do not exist!";
            }
            
            store.Categories = existingCategories;
            await _storeRepository.CreateAsync(store);
            
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
            
            await _storeRepository.UpdateAsync(store);
            
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
            var store = await _storeRepository.GetAsync(s => s.Id == storeId, tracked: false);
            
            await _storeRepository.RemoveAsync(store);
            
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