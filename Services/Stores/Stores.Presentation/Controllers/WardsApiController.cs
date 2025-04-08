namespace ShopeeFoodClone.WebApi.Stores.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/wards")]
[ApiVersion(1)]
public class WardsApiController : ControllerBase
{
    private readonly IWardService _service;
    private readonly ILogger<WardsApiController> _logger;
    private Response _response;

    public WardsApiController(
        IWardService service,
        ILogger<WardsApiController> logger)
    {
        _service = service;
        _logger = logger;
        _response = new Response();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetNames([FromQuery] string district, [FromQuery] int pageSize = 0, [FromQuery] int pageNumber = 1)
    {
        try
        {
            _logger.LogInformation("Getting the wards...");
            
            _response = await _service.GetNamesAsync(district: district, pageSize: pageSize, pageNumber: pageNumber);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the wards!");
        }
    }
}
