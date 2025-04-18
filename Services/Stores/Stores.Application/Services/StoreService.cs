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
            var districts = request.LocationRequest.Districts;
            var wards = request.LocationRequest.Wards;
            var categoryName = request.CategoryName;
            var subCategoryNames = request.SubCategoryNames;
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;
            var searchText = request.SearchText.ToLower().Trim();

            Func<IQueryable<Store>, IQueryable<Store>> include = query =>
                query
                    .Include(s => s.Ward)
                    .ThenInclude(w => w!.District)
                    .ThenInclude(d => d!.Province)
                    .Include(s => s.SubCategories);

            Expression<Func<Store, bool>> filter = x =>
                (wards == null || !wards.Any() || wards.Contains(x.Ward!.Code)) &&
                (districts == null || !districts.Any() || districts.Contains(x.Ward!.District!.Code)) &&
                (province.IsNullOrEmpty() || x.Ward!.District!.Province!.Code == province) &&
                (categoryName.IsNullOrEmpty() || x.SubCategories.Any(c => c.Category!.CodeName == categoryName)) &&
                (subCategoryNames == null || !subCategoryNames.Any() ||
                 x.SubCategories.Any(c => subCategoryNames.Contains(c.CodeName))) &&
                (!request.IsPromoted || x.IsPromoted) &&
                (
                    searchText.IsNullOrEmpty() ||
                    EF.Functions.Collate(x.Name, "Latin1_General_CI_AI").ToLower().Contains(searchText) ||
                    x.SubCategories.Any(sc =>
                        EF.Functions.Collate(sc.Name, "Latin1_General_CI_AI").ToLower().Contains(searchText)
                    )
                );

            Func<IQueryable<Store>, IOrderedQueryable<Store>> orderBy = query =>
                query.OrderByDescending(s => s.IsPromoted);

            var stores = await _storeRepository.GetAllAsync(
                filter: filter,
                include: include,
                orderBy: orderBy,
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
            
            Func<IQueryable<Store>, IQueryable<Store>> include = query =>
                query
                    .Include(s => s.Ward)
                    .ThenInclude(w => w!.District)
                    .ThenInclude(d => d!.Province)
                    .Include(s => s.SubCategories);

            Expression<Func<Store, bool>> filter = x => x.UserId == vendorId && (!request.IsPromoted || x.IsPromoted);

            var stores =
                await _storeRepository
                    .GetAllAsync(filter: filter, include: include, tracked: false, pageSize: pageSize, pageNumber: pageNumber);

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
            Func<IQueryable<Store>, IQueryable<Store>> include = query =>
                query
                    .Include(s => s.Ward)
                    .ThenInclude(w => w!.District)
                    .ThenInclude(d => d!.Province);

            var store = await _storeRepository.GetAsync(s => s.Id == storeId, include: include, tracked: false);

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
    /// Get a store's name
    /// </summary>
    /// <param name="storeId">The ID of the store</param>
    /// <returns>The store's name</returns>
    public async Task<Response> GetNameAsync(Guid storeId)
    {
        var response = new Response();
        try
        {
            var store = await _storeRepository.GetAsync(s => s.Id == storeId, tracked: false);

            response.Body = store!.Name;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }

        return response;
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
            var ward = await _wardRepository.GetByCodeAsync(w => w.Code == request.WardCode);
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
    /// Update the store's metadata by vendor (store's owner)
    /// </summary>
    /// <param name="request">The store to update</param>
    /// <returns>The updated store</returns>
    public async Task<Response> VendorUpdateAsync(VendorUpdateStoreRequest request)
    {
        var response = new Response();

        try
        {
            var store = await _storeRepository.GetAsync(s => s.Id == request.Id);

            if (store is null)
            {
                response.IsSuccessful = false;
                response.Message = "Store not found!";

                return response;
            }

            if (store.State != StoreState.Active)
            {
                response.IsSuccessful = false;
                response.Message = "Store is not in active state!";

                return response;
            }

            if (store.ConcurrencyStamp != request.ConcurrencyStamp)
            {
                response.IsSuccessful = false;
                response.Message = "Concurrency conflict! Data was modified by another user!";

                return response;
            }

            var storeToUpdate = _mapper.Map<Store>(request);

            // TODO: track user who update
            storeToUpdate.LastUpdatedAt = DateTime.UtcNow;
            storeToUpdate.ConcurrencyStamp = Guid.NewGuid();

            await _storeRepository.UpdateAsync(storeToUpdate);

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
    /// Update the store's state by admin
    /// </summary>
    /// <param name="request">The store to update</param>
    /// <returns>The updated store</returns>
    public async Task<Response> AdminUpdateAsync(AdminUpdateStoreRequest request)
    {
        var response = new Response();

        try
        {
            var store = await _storeRepository.GetAsync(s => s.Id == request.Id);

            if (store is null)
            {
                response.IsSuccessful = false;
                response.Message = "Store not found!";

                return response;
            }

            if (store.ConcurrencyStamp != request.ConcurrencyStamp)
            {
                response.IsSuccessful = false;
                response.Message = "Concurrency conflict! Data was modified by another user!";

                return response;
            }

            // TODO: Cannot map
            // Cons: Mismatch Datatype, Prop Name,...
            var storeToUpdate = _mapper.Map<StoreDto>(store);

            storeToUpdate.IsPromoted = request.IsPromoted;
            storeToUpdate.State = request.State;
            storeToUpdate.LastUpdatedAt = DateTime.UtcNow;
            storeToUpdate.ConcurrencyStamp = Guid.NewGuid();

            await _storeRepository.UpdateAsync(_mapper.Map<Store>(storeToUpdate));

            response.Body = storeToUpdate;

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
    /// Deleted the store (Vendor)
    /// </summary>
    /// <param name="storeId">The store's ID to delete</param>
    /// <returns>The deleted store (change store's state to "deleted")</returns>
    public async Task<Response> VendorDeleteAsync(Guid storeId)
    {
        var response = new Response();

        try
        {
            var store = await _storeRepository.GetAsync(s => s.Id == storeId, tracked: false);

            if (store is null)
            {
                response.IsSuccessful = false;
                response.Message = "Store not found!";

                return response;
            }


            store.State = StoreState.Deleted;
            store.LastUpdatedAt = DateTime.UtcNow;

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
    /// Deleted the store (Admin)
    /// </summary>
    /// <param name="storeId">The store's ID to delete</param>
    /// <returns>The deleted store</returns>
    public async Task<Response> RemoveAsync(Guid storeId)
    {
        var response = new Response();

        try
        {
            var store = await _storeRepository.GetAsync(s => s.Id == storeId, tracked: false);

            if (store is null)
            {
                response.IsSuccessful = false;
                response.Message = "Store not found!";

                return response;
            }

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

    public Response GetStoresCount(GetStoresCountRequest request)
    {
        var response = new Response();
        var searchText = request.SearchText.ToLower().Trim();

        if (request.LocationRequest is not null)
        {
            var province = request.LocationRequest.Province;
            var districts = request.LocationRequest.Districts;
            var wards = request.LocationRequest.Wards;
            var categoryName = request.CategoryName;
            var subCategoryNames = request.SubCategoryNames;

            Expression<Func<Store, bool>> filter = x =>
                (wards == null || !wards.Any() || wards.Contains(x.Ward!.Code)) &&
                (districts == null || !districts.Any() || districts.Contains(x.Ward!.District!.Code)) &&
                (province.IsNullOrEmpty() || x.Ward!.District!.Province!.Code == province) &&
                (categoryName.IsNullOrEmpty() || x.SubCategories.Any(c => c.Category!.CodeName == categoryName)) &&
                (subCategoryNames == null || !subCategoryNames.Any() ||
                 x.SubCategories.Any(c => subCategoryNames.Contains(c.CodeName))) &&
                (!request.IsPromoted || x.IsPromoted) &&
                (
                    searchText.IsNullOrEmpty() ||
                    EF.Functions.Collate(x.Name, "Latin1_General_CI_AI").ToLower().Contains(searchText) ||
                    x.SubCategories.Any(sc =>
                        EF.Functions.Collate(sc.Name, "Latin1_General_CI_AI").ToLower().Contains(searchText)
                    )
                );

            response.Body = _storeRepository.GetCount(filter);
        }
        else
        {
            Expression<Func<Store, bool>> filter = x =>
                (!request.IsPromoted || x.IsPromoted) &&
                (
                    searchText.IsNullOrEmpty() ||
                    EF.Functions.Collate(x.Name, "Latin1_General_CI_AI").ToLower().Contains(searchText) ||
                    x.SubCategories.Any(sc =>
                        EF.Functions.Collate(sc.Name, "Latin1_General_CI_AI").ToLower().Contains(searchText)
                    )
                );


            response.Body = _storeRepository.GetCount(filter);
        }

        return response;
    }
}