namespace ShopeeFoodClone.WebApi.Stores.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/stores")]
[ApiVersion(1)]
public class CollectionsApiController : ControllerBase
{
    private readonly ICollectionService _service;
    private readonly ILogger<CollectionsApiController> _logger;
    private Response _response;

    public CollectionsApiController(
        ICollectionService service,
        ILogger<CollectionsApiController> logger)
    {
        _service = service;
        _logger = logger;
        _response = new Response();
    }
    
    [HttpPost("{province}")]
    public async Task<IActionResult> GetByLocation(
        string province,
        [FromBody] GetCollectionsByLocationRequest request,
        [FromQuery] int pageSize = 12, 
        [FromQuery] int pageNumber = 1)
    {
        try
        {
            _logger.LogInformation($"Getting the collections in province/city {province}...");

            _response = await _service.GetByLocationAsync(request, pageSize: pageSize, pageNumber: pageNumber);

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the collections!");
        }
    }

    [HttpPost("{province}/{category}")]
    public async Task<IActionResult> GetByLocationAndCategory(
        [FromBody] GetCollectionsRequest request, 
        int pageSize = 12, 
        int pageNumber = 1)
    {
        try
        {
            var province = request.LocationRequest.Province;
            
            _logger.LogInformation($"Getting the collection of category {request.CategoryName} in province/city {province}...");
            
            _response = await _service.GetByLocationAndCategoryAsync(request, pageSize: pageSize, pageNumber: pageNumber);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the collections!");
        }
    }
    
    [HttpGet("{collectionId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid collectionId)
    {
        try
        {
            _logger.LogInformation("Getting the collection...");
            
            _response = await _service.GetAsync(collectionId);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the collection!");
        }
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminOnly")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCollectionRequest request)
    {
        try
        {
            _logger.LogInformation($"Creating collection {request.Collection.Id}");
            
            _response = await _service.CreateAsync(request);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when creating the collection!");
        }
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminOnly")]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CollectionDto collectionDto)
    {
        try
        {
            _logger.LogInformation($"Updating collection {collectionDto.Id}");
            
            _response = await _service.UpdateAsync(collectionDto);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when updating the collection!");
        }
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminOnly")]
    [HttpDelete("{collectionId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid collectionId)
    {
        try
        {
            _logger.LogInformation($"Deleting collection {collectionId}");
            
            _response = await _service.RemoveAsync(collectionId);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when deleting the collection!");
        }
    }
}
