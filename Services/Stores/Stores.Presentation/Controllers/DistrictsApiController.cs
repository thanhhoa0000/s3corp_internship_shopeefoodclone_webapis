namespace ShopeeFoodClone.WebApi.Stores.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/districts")]
[ApiVersion(1)]
public class DistrictsApiController : ControllerBase
{
    private readonly IDistrictService _service;
    private readonly ILogger<DistrictsApiController> _logger;
    private Response _response;

    public DistrictsApiController(
        IDistrictService service,
        ILogger<DistrictsApiController> logger)
    {
        _service = service;
        _logger = logger;
        _response = new Response();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string province, [FromQuery] int pageSize = 0, [FromQuery] int pageNumber = 1)
    {
        try
        {
            _logger.LogInformation("Getting the districts...");
            
            _response = await _service.GetAllByProvinceAsync(province: province, pageSize:  pageSize, pageNumber: pageNumber);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the districts!");
        }
    }
}

