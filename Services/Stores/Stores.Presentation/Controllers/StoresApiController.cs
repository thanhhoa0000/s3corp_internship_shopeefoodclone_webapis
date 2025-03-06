namespace ShopeeFoodClone.WebApi.Stores.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/stores")]
[ApiVersion(1)]
public class StoresApiController : ControllerBase
{
    private readonly IStoreService _service;
    private readonly ILogger<StoresApiController> _logger;
    private Response _response;

    public StoresApiController(IStoreService service, ILogger<StoresApiController> logger)
    {
        _service = service;
        _logger = logger;
        _response = new Response();
    }
    
    [HttpGet("{province}")]
    public async Task<IActionResult> GetByLocation(
        GetStoreByLocationRequest request,
        int pageSize = 12, 
        int pageNumber = 1)
    {
        try
        {
            _logger.LogInformation($"Getting the stores in province/city {request.Province}...");

            _response = await _service.GetByLocationAsync(request, pageSize: pageSize, pageNumber: pageNumber);

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the stores!");
        }
    }

    [HttpGet("{province}/{category}")]
    public async Task<IActionResult> GetByLocationAndCategory(
        [FromBody] GetStoreRequest request, 
        int pageSize = 12, 
        int pageNumber = 1)
    {
        try
        {
            var province = request.LocationRequest.Province;
            var district = request.LocationRequest.District;
            var ward = request.LocationRequest.Ward;
            var street = request.LocationRequest.Street;
            
            _logger.LogInformation($"Getting the stores of category {request.CategoryName} in province/city {province}...");
            
            _response = await _service.GetByLocationAndCategoryAsync(request, pageSize: pageSize, pageNumber: pageNumber);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the stores!");
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminAndVendorOnly")]
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetAllByUserId([FromRoute] Guid userId, int pageSize = 12, int pageNumber = 1)
    {
        try
        {
            _logger.LogInformation($"Getting the stores of user {userId}...");
            
            _response = await _service.GetAllByUserIdAsync(userId: userId, pageSize: pageSize, pageNumber: pageNumber);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the stores!");
        }
    }
    
    [HttpGet("{storeId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid storeId)
    {
        try
        {
            _logger.LogInformation("Getting the store...");
            
            _response = await _service.GetAsync(storeId);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the store!");
        }
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "VendorOnly")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStoreRequest request)
    {
        try
        {
            _logger.LogInformation($"Creating store {request.Store.Id}");
            
            _response = await _service.CreateAsync(request);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when creating the store!");
        }
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "VendorOnly")]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] StoreDto storeDto)
    {
        try
        {
            _logger.LogInformation($"Updating store {storeDto.Id}");
            
            _response = await _service.UpdateAsync(storeDto);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when updating the store!");
        }
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "VendorOnly")]
    [HttpDelete("{storeId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid storeId)
    {
        try
        {
            _logger.LogInformation($"Deleting store {storeId}");
            
            _response = await _service.RemoveAsync(storeId);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when deleting the store!");
        }
    }
}
