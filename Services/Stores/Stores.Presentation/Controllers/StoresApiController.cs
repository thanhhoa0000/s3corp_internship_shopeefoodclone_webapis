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

    // TODO: post
    [HttpGet]
    public async Task<IActionResult> GetByLocationAndCategory(
        [FromQuery] string? province,
        [FromQuery] string? district,
        [FromQuery] string? ward,
        [FromQuery] string? category,
        [FromQuery] int pageSize = 12, 
        [FromQuery] int pageNumber = 1)
    {
        try
        {
            var request = new GetStoresRequest()
            {
                LocationRequest = new LocationRequest()
                {
                    Province = province,
                    District = district,
                    Ward = ward,
                },
                CategoryName = category,
                PageSize = pageSize,
                PageNumber = pageNumber
            };
            
            if (category is null)
                _logger.LogInformation($"Getting the stores in province/city {province}...");
            else
                _logger.LogInformation($"Getting the stores of category {request.CategoryName} in province/city {province}...");
            
            _response = await _service.GetByLocationAndCategoryAsync(request);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the stores!");
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminAndVendorOnly")]
    [HttpGet("vendor/{vendorId}")]
    public async Task<IActionResult> GetAllByUserId(
        [FromRoute] Guid vendorId, 
        [FromQuery] int pageSize = 12,
        [FromQuery] int pageNumber = 1)
    {
        try
        {
            _logger.LogInformation($"Getting the stores of user {vendorId}...");

            var request = new GetStoresByVendorIdRequest()
            {
                VendorId = vendorId,
                PageSize = pageSize,
                PageNumber = pageNumber
            };
            
            _response = await _service.GetAllByVendorIdAsync(request);
            
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
