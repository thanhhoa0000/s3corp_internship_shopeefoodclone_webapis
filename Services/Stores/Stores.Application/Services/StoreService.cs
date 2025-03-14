using Microsoft.EntityFrameworkCore;

namespace ShopeeFoodClone.WebApi.Stores.Application.Services;

public class StoreService : IStoreService
{
    private readonly IStoreRepository _storeRepository;
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly IWardRepository _wardRepository;
    private readonly IMapper _mapper;

    public StoreService(
        IStoreRepository repository,
        ISubCategoryRepository subCategoryRepository,
        IWardRepository wardRepository,
        IMapper mapper)
    {
        _storeRepository = repository;
        _subCategoryRepository = subCategoryRepository;
        _wardRepository = wardRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Get list of stores base on location
    /// </summary>
    /// <param name="request">Location to get</param>
    /// <param name="pageSize">Maximum number of stores per page</param>
    /// <param name="pageNumber">Page number to start with</param>
    /// <returns>The stores list</returns>
    public async Task<Response> GetByLocationAsync(GetStoreByLocationRequest request, int pageSize = 12,
        int pageNumber = 1)
    {
        var response = new Response();

        try
        {
            Expression<Func<Store, bool>> filter = x =>
                (string.IsNullOrEmpty(request.Ward) || x.Ward!.CodeName == request.Ward) &&
                (string.IsNullOrEmpty(request.District) || x.Ward!.District!.CodeName == request.District) &&
                (string.IsNullOrEmpty(request.Province) || x.Ward!.District!.Province!.CodeName == request.Province);
            
            var stores = await _storeRepository.GetAllAsync(filter: filter, pageSize: pageSize, pageNumber: pageNumber);
            
            response.Body = _mapper.Map<IEnumerable<StoreDto>>(stores);
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }
        
        return response;
    }

    /// <summary>
    /// Get list of stores base on location and category
    /// </summary>
    /// <param name="request">Location and category name to get</param>
    /// <param name="pageSize">Maximum number of stores per page</param>
    /// <param name="pageNumber">Page number to start with</param>
    /// <returns>The stores list</returns>
    public async Task<Response> GetByLocationAndCategoryAsync(
        GetStoreRequest request,
        int pageSize = 12, int pageNumber = 1)
    {
        var response = new Response();

        try
        {
            var province = request.LocationRequest.Province;
            var district = request.LocationRequest.District;
            var ward = request.LocationRequest.Ward;

            Func<IQueryable<Store>, IQueryable<Store>>? include = query =>
                query
                    .Include(s => s.Ward)
                    .ThenInclude(w => w!.District)
                    .ThenInclude(d => d!.Province)
                    .Include(s => s.SubCategories)
                    .ThenInclude(sc => sc.Category);
            
            Expression<Func<Store, bool>> filter = x => 
                (string.IsNullOrEmpty(ward) || x.Ward!.CodeName == ward) &&
                (string.IsNullOrEmpty(district) || x.Ward!.District!.CodeName == district) &&
                (string.IsNullOrEmpty(province) || x.Ward!.District!.Province!.CodeName == province) &&
                (x.SubCategories.Any(c => c.Category!.CodeName == request.CategoryName));
            
            var stores = await _storeRepository.GetAllAsync(
                filter: filter, 
                include: include,
                pageSize: pageSize, 
                pageNumber: pageNumber);
            
            response.Body = _mapper.Map<IEnumerable<StoreDto>>(stores);
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }
        
        return response;
    }
    
    
    /// <summary>
    /// Get stores list by owner's userId
    /// </summary>
    /// <param name="userId">The owner's userId</param>
    /// <param name="pageSize">Pages number to get roles</param>
    /// <param name="pageNumber">Page number to start with</param>
    /// <returns>The stores list</returns>
    public async Task<Response> GetAllByUserIdAsync(Guid userId, int pageSize = 12, int pageNumber = 1)
    {
        var response = new Response();

        try
        {
            var stores = await _storeRepository.GetAllAsync(s => s.UserId == userId, tracked: false, pageSize: pageSize, pageNumber: pageNumber);
            
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
            
            var existingSubCategories = (await _subCategoryRepository.GetAllAsync(c => request.SubCateIds.Contains(c.Id))).ToList();

            if (existingSubCategories.Count != request.SubCateIds.Count)
            {
                response.IsSuccessful = false;
                response.Message = "One or more categories do not exist!";
            }
            
            store.SubCategories = existingSubCategories;
            var ward = await _wardRepository.GetByCodeAsync(
                w => w.Code == request.WardCode);
            store.Ward = ward;
            store.WardCode = ward.Code;
            store.Id = Guid.NewGuid();
            store.CoverImagePath = $"/stores/{store.Id}/cover-img.jpg";
            await _storeRepository.CreateAsync(store);
            
            response.Body = _mapper.Map<StoreDto>(store);
            
            return response;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.ToString();
            
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