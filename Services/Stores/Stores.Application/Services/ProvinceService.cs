using ShopeeFoodClone.WebApi.Stores.Domain.Entities;

namespace ShopeeFoodClone.WebApi.Stores.Application.Services;

public class ProvinceService : IProvinceService
{
    private readonly IProvinceRepository _provinceRepository;
    private readonly IStoreRepository _storeRepository;
    private readonly IMapper _mapper;

    public ProvinceService(
        IProvinceRepository provinceRepository, 
        IStoreRepository storeRepository,
        IMapper mapper)
    {
        _provinceRepository = provinceRepository;
        _storeRepository = storeRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Get list of provinces
    /// </summary>
    /// <param name="pageSize">Pages number to get provinces</param>
    /// <param name="pageNumber">Page number to start with</param>
    /// <returns>The provinces list</returns>
    public async Task<Response> GetAllAsync(int pageSize = 0, int pageNumber = 1)
    {
        var response = new Response();

        try
        {
            var provinces = await _provinceRepository.GetAllAsync(tracked: false, pageSize: pageSize, pageNumber: pageNumber);
            
            response.Body = _mapper.Map<IEnumerable<ProvinceDto>>(provinces);
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }
        
        return response;
    }

    /// <summary>
    /// Get list of provinces' name with stores count
    /// </summary>
    /// <param name="pageSize">Pages number to get provinces</param>
    /// <param name="pageNumber">Page number to start with</param>
    /// <returns>The provinces list</returns>
    public async Task<Response> GetNamesWithStoresCountAsync(int pageSize = 0, int pageNumber = 1)
    {
        var response = new Response();

        try
        {
            var provinces = await _provinceRepository.GetAllAsync(tracked: false, pageSize: pageSize, pageNumber: pageNumber);
            var returnObjects = new List<object>();


            foreach (var province in provinces)
            {
                Expression<Func<Store, bool>> storeFilter = x => x.Ward!.District!.Province!.Code == province.Code;
                var storesCount = (await _storeRepository.GetAllAsync(filter: storeFilter, pageSize: 0, pageNumber: 1)).ToList().Count;

                returnObjects.Add(new
                {
                    Name = province.Name,
                    CodeName = province.CodeName,
                    Code = province.Code,
                    StoresCount = storesCount
                });
            }

            response.Body = returnObjects;
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
