namespace ShopeeFoodClone.WebApi.Stores.Application.Services;

public class ProvinceService : IProvinceService
{
    private readonly IProvinceRepository _repository;
    private readonly IMapper _mapper;

    public ProvinceService(IProvinceRepository repository, IMapper mapper)
    {
        _repository = repository;
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
            var provinces = await _repository.GetAllAsync(tracked: false, pageSize: pageSize, pageNumber: pageNumber);
            
            response.Body = _mapper.Map<IEnumerable<ProvinceDto>>(provinces);
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }
        
        return response;
    }
}