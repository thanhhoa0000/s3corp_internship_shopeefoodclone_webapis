namespace ShopeeFoodClone.WebApi.Products.Presentation.Controllers;

[AllowAnonymous]
[Authorize(AuthenticationSchemes = "Bearer", Policy = "VendorOnly")]
[ApiController]
[Route("api/v{version:apiVersion}/menus")]
[ApiVersion(1)]
public class MenusApiController : ControllerBase
{
    private readonly IMenuService _service;
    private readonly ILogger<MenusApiController> _logger;
    private Response _response;

    public MenusApiController(
        IMenuService service,
        ILogger<MenusApiController> logger)
    {
        _service = service;
        _logger = logger;
        _response = new Response();
    }

    [AllowAnonymous]
    [HttpGet("{menuId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid menuId)
    {
        try
        {
            _logger.LogInformation($"Getting the menu {menuId}...");
            
            _response = await _service.GetAsync(menuId);
            
            if (_response.Message.Contains("not found"))
            {
                return NotFound("Menu not found!");
            }
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the menu!");
        }
    }

    [AllowAnonymous]
    [HttpPost("get-from-store")]
    public async Task<IActionResult> GetMenusByStoreId([FromBody] GetMenusRequest request)
    {
        try
        {
            _logger.LogInformation($"Getting the menus of store {request.StoreId}...");
            
            _response = await _service.GetMenusByStoreIdAsync(request);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the menus!");
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMenuRequest request)
    {
        try
        {
            _logger.LogInformation($"Creating menu {request.Title}...");
            
            _response = await _service.CreateAsync(request);

            return Created();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when creating the menu!");
        }
    }

    [HttpPost("add-products")]
    public async Task<IActionResult> AddProducts([FromBody] VendorAddProductsToMenuRequest request)
    {
        try
        {
            _logger.LogInformation($"Add product(s) to menu {request.MenuId}...");
            
            _response = await _service.AddProductsToMenuAsync(request);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when updating the menu!");
        }
    }
    
    [HttpPut("update-metadata")]
    public async Task<IActionResult> VendorUpdate([FromBody] VendorUpdateMenuRequest request)
    {
        try
        {
            _logger.LogInformation($"Updating menu {request.Title}...");
            
            _response = await _service.UpdateMenuAsync(request);
            
            if (_response.Message.Contains("not found"))
            {
                return NotFound("Role not found!");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when updating the menu!");
        }
    }
    
    [HttpPut("update-state")]
    public async Task<IActionResult> VendorStateUpdate([FromBody] VendorUpdateMenuStateRequest request)
    {
        try
        {
            _logger.LogInformation($"Updating menu {request.MenuId}...");
            
            _response = await _service.UpdateMenuStateAsync(request);
            
            if (_response.Message.Contains("not found"))
            {
                return NotFound("Role not found!");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when updating the menu!");
        }
    }
    
    [HttpDelete("delete/{menuId:guid}")]
    public async Task<IActionResult> VendorDelete([FromRoute] Guid menuId)
    {
        try
        {
            _logger.LogInformation($"Deleting menu {menuId}...");
            
            _response = await _service.VendorDeleteAsync(menuId);
            
            if (_response.Message.Contains("not found"))
            {
                return NotFound("Role not found!");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when deleting the menu!");
        }
    }
    
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "AdminOnly")]
    [HttpDelete("remove/{menuId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid menuId)
    {
        try
        {
            _logger.LogInformation($"Deleting menu {menuId}...");
            
            _response = await _service.RemoveAsync(menuId);
            
            if (_response.Message.Contains("not found"))
            {
                return NotFound("Role not found!");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when deleting the menu!");
        }
    }
}
