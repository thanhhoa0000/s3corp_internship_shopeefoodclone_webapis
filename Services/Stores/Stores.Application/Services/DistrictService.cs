namespace ShopeeFoodClone.WebApi.Stores.Application.Services;

public class DistrictService : IDistrictService
{
    private readonly IDistrictRepository _repository;
    private readonly IMapper _mapper;

    public DistrictService(IMapper mapper, IDistrictRepository repository)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Get list of districts
    /// </summary>
    /// <param name="province">The province to get districts</param>
    /// <param name="pageSize">Pages number to get districts</param>
    /// <param name="pageNumber">Page number to start with</param>
    /// <returns>The districts list</returns>
    public async Task<Response> GetAllByProvinceAsync(string province, int pageSize = 0, int pageNumber = 1)
    {
        var response = new Response();

        try
        {
            var districts = await _repository.GetAllAsync(d => d.Province!.Code == province, tracked: false, pageSize: pageSize, pageNumber: pageNumber);
            
            response.Body = _mapper.Map<IEnumerable<DistrictDto>>(districts);
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
        }
        
        return response;
    }
}