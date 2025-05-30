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
    
    [HttpPost("get")]
    public async Task<IActionResult> GetByLocationAndCategory([FromBody] GetStoresRequest request)
    {
        try
        {
            var province = request.LocationRequest!.Province;
            var category =  request.CategoryName;
            var subCategories = request.SubCategoryNames;
            
            if ((subCategories.IsNullOrEmpty() || !subCategories!.Any()) && category.IsNullOrEmpty())
                _logger.LogInformation($"Getting the stores in province/city {province}...");
            else if (!category.IsNullOrEmpty())
                _logger.LogInformation($"Getting the stores in of category {category} in province/city {province}...");
            else
                _logger.LogInformation($"Getting the stores of sub-categories {string.Join(", ", subCategories!)} in province/city {province}...");
            
            _response = await _service.GetByLocationAndCategoryAsync(request);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the stores!");
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "VendorOnly")]
    [HttpPost("vendor")]
    public async Task<IActionResult> GetAllByVendorId(GetStoresByVendorIdRequest request)
    {
        try
        {
            _logger.LogInformation($"Getting the stores of vendor {request.VendorId}...");
            
            _response = await _service.GetAllByVendorIdAsync(request);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the stores!");
        }
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminOnly")]
    [HttpPost("of-vendor/")]
    public async Task<IActionResult> AdminGetAllByVendorId(GetStoresByVendorIdRequest request)
    {
        try
        {
            _logger.LogInformation($"Getting the stores of vendor {request.VendorId}...");
            
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
    
    [HttpGet("get-name/{storeId:guid}")]
    public async Task<IActionResult> GetNameById([FromRoute] Guid storeId)
    {
        try
        {
            _logger.LogInformation("Getting the store's name...");
            
            _response = await _service.GetNameAsync(storeId);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the store's name!");
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
            
            return Created();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when creating the store!");
        }
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "VendorOnly")]
    [HttpPut("update-by-vendor")]
    public async Task<IActionResult> VendorUpdate([FromBody] VendorUpdateStoreRequest request)
    {
        try
        {
            _logger.LogInformation($"Updating store {request.Id}...");
            
            _response = await _service.VendorUpdateAsync(request);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when updating the store!");
        }
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminOnly")]
    [HttpPut("update-by-admin")]
    public async Task<IActionResult> AdminUpdate([FromBody] AdminUpdateStoreRequest request)
    {
        try
        {
            _logger.LogInformation($"Updating store {request.Id}...");
            
            _response = await _service.AdminUpdateAsync(request);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when updating the store!");
        }
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "VendorOnly")]
    [HttpDelete("delete-by-vendor/{storeId:guid}")]
    public async Task<IActionResult> VendorDelete([FromRoute] Guid storeId)
    {
        try
        {
            _logger.LogInformation($"Deleting store {storeId}...");
            
            _response = await _service.VendorDeleteAsync(storeId);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when deleting the store!");
        }
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminOnly")]
    [HttpDelete("delete-by-admin/{storeId:guid}")]
    public async Task<IActionResult> AdminDelete([FromRoute] Guid storeId)
    {
        try
        {
            _logger.LogInformation($"Deleting store {storeId}...");
            
            _response = await _service.RemoveAsync(storeId);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when deleting the store!");
        }
    }

    [HttpPost("get-count")]
    public IActionResult GetStoresCount([FromBody] GetStoresCountRequest request)
    {
        try
        {
            _logger.LogInformation($"Getting stores count...");
            
            return Ok(_service.GetStoresCount(request));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the stores count!");
        }
    }
}
