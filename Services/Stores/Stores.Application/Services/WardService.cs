namespace ShopeeFoodClone.WebApi.Stores.Application.Services;

public class WardService : IWardService
{
    private readonly IWardRepository _wardRepository;
    private readonly IMapper _mapper;

    public WardService(
        IWardRepository wardRepository,
        IMapper mapper)
    {
        _wardRepository = wardRepository;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Get list of provinces
    /// </summary>
    /// <param name="pageSize">Pages number to get wards</param>
    /// <param name="pageNumber">Page number to start with</param>
    /// <returns>The wards list</returns>
    public async Task<Response> GetNamesAsync(string district, int pageSize = 0, int pageNumber = 1)
    {
        var response = new Response();
        try
        {
            var returnObjects = new List<object>();

            var wards = await _wardRepository.GetAllAsync(
                filter: w => w.District!.Code == district,
                tracked: false, 
                pageSize: pageSize, 
                pageNumber: pageNumber);

            foreach (var ward in wards)
            {
                returnObjects.Add(new {
                    Name = ward.Name,
                    Code = ward.Code
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
