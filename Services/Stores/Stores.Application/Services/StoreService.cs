using Microsoft.IdentityModel.Tokens;

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
    /// Get list of stores base on location and category
    /// </summary>
    /// <param name="request">Location and category name to get</param>
    /// <returns>The stores list</returns>
    public async Task<Response> GetByLocationAndCategoryAsync(GetStoresRequest request)
    {
        var response = new Response();

        try
        {
            var province = request.LocationRequest!.Province;
            var district = request.LocationRequest.District;
            var ward = request.LocationRequest.Ward;
            var categoryName = request.CategoryName;
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;

            Func<IQueryable<Store>, IQueryable<Store>>? include = query =>
                query
                    .Include(s => s.Ward)
                    .ThenInclude(w => w!.District)
                    .ThenInclude(d => d!.Province)
                    .Include(s => s.SubCategories)
                    .ThenInclude(sc => sc.Category);

            Expression<Func<Store, bool>> filter = x =>
                (ward.IsNullOrEmpty() || x.Ward!.Code == ward) &&
                (district.IsNullOrEmpty() || x.Ward!.District!.Code == district) &&
                (province.IsNullOrEmpty() || x.Ward!.District!.Province!.Code == province) &&
                (categoryName.IsNullOrEmpty() || x.SubCategories.Any(c => c.Category!.CodeName == categoryName));

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
    /// Get stores list by vendor's userId
    /// </summary>
    /// <param name="request">The vendor's userId</param>
    /// <returns>The stores list</returns>
    public async Task<Response> GetAllByVendorIdAsync(GetStoresByVendorIdRequest request)
    {
        var response = new Response();

        try
        {
            var vendorId = request.VendorId;
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;

            Expression<Func<Store, bool>> filter = x => x.UserId == vendorId;

            var stores =
                await _storeRepository
                    .GetAllAsync(filter: filter, tracked: false, pageSize: pageSize, pageNumber: pageNumber);

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

            var existingSubCategories =
                (await _subCategoryRepository.GetAllAsync(c => request.SubCateIds.Contains(c.Id))).ToList();

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
            // TODO: check existance, and state with corresponding behaviours (Validation model)
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
    /// <param name="storeId">The store's ID to delete</param>
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