namespace ShopeeFoodClone.WebApi.Stores.Application.Services;

public class CollectionService : ICollectionService
{
    private readonly ICollectionRepository _collectionRepository;
    private readonly IStoreRepository _storeRepository;
    private readonly IWardRepository _wardRepository;
    private readonly IMapper _mapper;

    public CollectionService(
        ICollectionRepository collectionRepository,
        IStoreRepository storeRepository,
        IWardRepository wardRepository,
        IMapper mapper)
    {
        _collectionRepository = collectionRepository;
        _storeRepository = storeRepository;
        _wardRepository = wardRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Get list of collections base on location
    /// </summary>
    /// <param name="request">Location to get</param>
    /// <param name="pageSize">Maximum number of collections per page</param>
    /// <param name="pageNumber">Page number to start with</param>
    /// <returns>The stores list</returns>
    public async Task<Response> GetByLocationAsync(
        GetCollectionsByLocationRequest request, 
        int pageSize = 0,
        int pageNumber = 1)
    {
        var response = new Response();

        try
        {
            Expression<Func<Collection, bool>> filter = x =>
                (string.IsNullOrEmpty(request.Ward) || x.Stores!.Any(s => s.Ward!.CodeName == request.Ward)) &&
                (string.IsNullOrEmpty(request.District) || x.Stores!.Any(s => s.Ward!.District!.CodeName == request.District)) &&
                (string.IsNullOrEmpty(request.Province) || x.Stores!.Any(s => s.Ward!.District!.Province!.CodeName == request.Province));
            
            var collections = await _collectionRepository.GetAllAsync(filter: filter, pageSize: pageSize, pageNumber: pageNumber);
            
            response.Body = _mapper.Map<IEnumerable<CollectionDto>>(collections);
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }
        
        return response;
    }
    
    /// <summary>
    /// Get list of collections base on location and category
    /// </summary>
    /// <param name="request">Location and category name to get</param>
    /// <param name="pageSize">Maximum number of collections per page</param>
    /// <param name="pageNumber">Page number to start with</param>
    /// <returns>The collections list</returns>
    public async Task<Response> GetByLocationAndCategoryAsync(
        GetCollectionsRequest request,
        int pageSize = 12,
        int pageNumber = 1)
    {
        var response = new Response();

        try
        {
            var province = request.LocationRequest.Province;
            var district = request.LocationRequest.District;
            var ward = request.LocationRequest.Ward;

            Func<IQueryable<Collection>, IQueryable<Collection>>? include = query =>
                query
                    .Include(c => c.Stores!)
                    .ThenInclude(s => s.Ward)
                    .ThenInclude(w => w!.District)
                    .ThenInclude(d => d!.Province);
            
            Expression<Func<Collection, bool>> filter = x => 
                (string.IsNullOrEmpty(ward) || x.Stores!.Any(s => s.Ward!.CodeName == ward)) &&
                (string.IsNullOrEmpty(district) || x.Stores!.Any(s => s.Ward!.District!.CodeName == district)) &&
                (string.IsNullOrEmpty(province) || x.Stores!.Any(s => s.Ward!.District!.Province!.CodeName == province)) &&
                (x.Stores!.Any(s => s.SubCategories.Any(c => c.CodeName == request.CategoryName)));
            
            var collections = await _collectionRepository.GetAllAsync(
                filter: filter, 
                include: include,
                pageSize: pageSize, 
                pageNumber: pageNumber);
            
            response.Body = _mapper.Map<IEnumerable<CollectionDto>>(collections);
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }
        
        return response;
    }
    
    /// <summary>
    /// Get a collection
    /// </summary>
    /// <param name="collectionId">The ID of the collection</param>
    /// <returns>The collection</returns>
    public async Task<Response> GetAsync(Guid collectionId)
    {
        var response = new Response();

        try
        {
            var store = await _collectionRepository.GetAsync(
                c => c.Id == collectionId,
                q => q
                    .Include(c => c.Stores!)
                    .ThenInclude(s => s.Ward)
                    .ThenInclude(w => w!.District)
                    .ThenInclude(d => d!.Province),
                tracked: false);
            
            response.Body = _mapper.Map<CollectionDto>(store);
            
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
    /// Create a Collection
    /// </summary>
    /// <param name="request">The Collection to create and the needed information</param>
    /// <returns>The created Collection</returns>
    public async Task<Response> CreateAsync(CreateCollectionRequest request)
    {
        var response = new Response();

        try
        {
            var collection = _mapper.Map<Collection>(request.Collection);
            
            var existingStores = (await _storeRepository.GetAllAsync(s => request.StoreIds.Contains(s.Id))).ToList();

            if (existingStores.Count != request.StoreIds.Count)
            {
                response.IsSuccessful = false;
                response.Message = "One or more stores do not exist!";
            }
            
            collection.Stores = existingStores;
            
            collection.Id = Guid.NewGuid();
            collection.CoverImagePath = $"/collections/{collection.Id}/cover-img.jpg";
            await _collectionRepository.CreateAsync(collection);
            
            response.Body = _mapper.Map<CollectionDto>(collection);
            
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
    /// Update the collection's metadata
    /// </summary>
    /// <param name="collectionDto">The collection to update</param>
    /// <returns>The updated store</returns>
    public async Task<Response> UpdateAsync(CollectionDto collectionDto)
    {
        var response = new Response();

        try
        {
            var collection = _mapper.Map<Collection>(collectionDto);
            
            await _collectionRepository.UpdateAsync(collection);
            
            response.Body = _mapper.Map<CollectionDto>(collection);
            
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
    /// Deleted the collection
    /// </summary>
    /// <param name="collectionId">The collection's ID to collection</param>
    /// <returns>The deleted collection</returns>
    public async Task<Response> RemoveAsync(Guid collectionId)
    {
        var response = new Response();

        try
        {
            var collection = await _collectionRepository.GetAsync(c => c.Id == collectionId, tracked: false);
            
            await _collectionRepository.RemoveAsync(collection);
            
            response.Body = _mapper.Map<CollectionDto>(collection);
            
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