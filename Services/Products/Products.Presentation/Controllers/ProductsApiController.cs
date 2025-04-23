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
    [HttpPost("get-from-store")]
    public async Task<IActionResult> GetAllByStoreId([FromBody] GetProductsRequest request)
    {
        try
        {
            _logger.LogInformation($"Getting the products of store {request.StoreId}...");
            
            _response = await _service.GetAllByStoreIdAsync(request);
            
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
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        try
        {
            _logger.LogInformation($"Creating product {request.Id}...");
            
            _response = await _service.CreateAsync(request);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when creating the product!");
        }
    }
    
    [HttpPut("update-metadata")]
    public async Task<IActionResult> VendorUpdate([FromBody] VendorUpdateProductRequest request)
    {
        try
        {
            _logger.LogInformation($"Updating product {request.Id}...");
            
            _response = await _service.VendorUpdateAsync(request);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when updating the product!");
        }
    }
    
    [HttpPut("update-state")]
    public async Task<IActionResult> VendorStateUpdate([FromBody] VendorUpdateProductStateRequest request)
    {
        try
        {
            _logger.LogInformation($"Updating product {request.ProductId}...");
            
            _response = await _service.VendorChangeProductStateAsync(request);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when updating the product!");
        }
    }
    
    [HttpDelete("delete/{productId:guid}")]
    public async Task<IActionResult> VendorDelete([FromRoute] Guid productId)
    {
        try
        {
            _logger.LogInformation($"Deleting product {productId}...");
            
            _response = await _service.VendorDeleteAsync(productId);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when deleting the product!");
        }
    }
    
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "AdminOnly")]
    [HttpDelete("remove/{productId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid productId)
    {
        try
        {
            _logger.LogInformation($"Deleting product {productId}...");
            
            _response = await _service.RemoveAsync(productId);
            
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when deleting the product!");
        }
    }
}
