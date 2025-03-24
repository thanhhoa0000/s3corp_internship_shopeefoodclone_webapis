namespace ShopeeFoodClone.WebApi.Stores.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/collections")]
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
    
    [HttpPost("get")]
    public async Task<IActionResult> GetByLocationAndCategory([FromBody] GetCollectionsRequest request)
    {
        try
        {
            var province = request.LocationRequest!.Province;
            var category = request.CategoryName;
            
            if (category is null)
                _logger.LogInformation($"Getting the collections in province/city {province}...");
            else
                _logger.LogInformation($"Getting the collections of category {category} in province/city {province}...");
            
            _response = await _service.GetByLocationAndCategoryAsync(request);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the stores!");
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
            _logger.LogInformation($"Creating collection {request.Collection!.Id}");
            
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
    
    // TODO: Soft deleting?
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
