namespace ShopeeFoodClone.WebApi.Stores.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/provinces")]
[ApiVersion(1)]
public class ProvincesApiController : ControllerBase
{
    private readonly IProvinceService _service;
    private readonly ILogger<ProvincesApiController> _logger;
    private Response _response;

    public ProvincesApiController(IProvinceService service, ILogger<ProvincesApiController> logger)
    {
        _service = service;
        _logger = logger;
        _response = new Response();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageSize = 0, [FromQuery] int pageNumber = 1)
    {
        try
        {
            _logger.LogInformation("Getting the provinces...");
            
            _response = await _service.GetAllAsync(pageSize:  pageSize, pageNumber: pageNumber);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the provinces!");
        }
    }

    [HttpGet("with-stores-count")]
    public async Task<IActionResult> GetNamesWithStoresCount([FromQuery] int pageSize = 0, [FromQuery] int pageNumber = 1)
    {
        try
        {
            _logger.LogInformation("Getting the provinces with stores count...");

            _response = await _service.GetNamesWithStoresCountAsync(pageSize: pageSize, pageNumber: pageNumber);

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);

            return BadRequest("Error(s) occurred when getting the provinces!");
        }
    }
}
