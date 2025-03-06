namespace ShopeeFoodClone.WebApi.Products.Presentation.Controllers;

[AllowAnonymous]
[Authorize(AuthenticationSchemes = "Bearer", Policy = "VendorOnly")]
[ApiController]
[Route("api/v{version:apiVersion}/products")]
[ApiVersion(1)]
public class ProductsApiController : ControllerBase
{
    private readonly IProductService _service;
    private readonly ILogger<ProductsApiController> _logger;
    private Response _response;

    public ProductsApiController(IProductService service, ILogger<ProductsApiController> logger)
    {
        _service = service;
        _logger = logger;
        _response = new Response();
    }
    
    [AllowAnonymous]
    [HttpGet("from-store/{storeId}")]
    public async Task<IActionResult> GetAllByStoreId([FromRoute] Guid storeId, int pageSize = 12, int pageNumber = 1)
    {
        try
        {
            _logger.LogInformation($"Getting the products of store {storeId}...");
            
            _response = await _service.GetAllByStoreIdAsync(storeId: storeId, pageSize: pageSize, pageNumber: pageNumber);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the products!");
        }
    }
    
    [AllowAnonymous]
    [HttpGet("{productId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid productId)
    {
        try
        {
            _logger.LogInformation("Getting the product...");
            
            _response = await _service.GetAsync(productId);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the product!");
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductDto productDto)
    {
        try
        {
            _logger.LogInformation($"Creating product {productDto.Id}");
            
            _response = await _service.CreateAsync(productDto);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when creating the product!");
        }
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProductDto productDto)
    {
        try
        {
            _logger.LogInformation($"Updating product {productDto.Id}");
            
            _response = await _service.UpdateAsync(productDto);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when updating the product!");
        }
    }
    
    [HttpDelete("{productId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid productId)
    {
        try
        {
            _logger.LogInformation($"Deleting product {productId}");
            
            _response = await _service.RemoveAsync(productId);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when deleting the product!");
        }
    }
}
