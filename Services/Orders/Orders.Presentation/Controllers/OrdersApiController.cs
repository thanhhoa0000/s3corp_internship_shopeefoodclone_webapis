namespace ShopeeFoodClone.WebApi.Orders.Presentation.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "CustomerOnly")]
[ApiController]
[Route("api/v{version:apiVersion}/orders")]
[ApiVersion(1)]
public class OrdersApiController : ControllerBase
{
    private readonly IOrderService _service;
    private readonly ILogger<OrdersApiController> _logger;
    private Response _response;

    public OrdersApiController(IOrderService service, ILogger<OrdersApiController> logger)
    {
        _service = service;
        _logger = logger;
        _response = new Response();
    }

    [HttpPost("get")]
    public async Task<IActionResult> GetByCustomerId([FromBody] GetOrdersRequest request)
    {
        try
        {
            _logger.LogInformation($"Getting the orders of customer {request.CustomerId}...");

            _response = await _service.GetOrdersByCustomerIdAsync(request);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the orders!");
        }
    }

    [HttpGet("{orderId:guid}")]
    public async Task<IActionResult> GetById(Guid orderId)
    {
        try
        {
            _logger.LogInformation($"Getting the order {orderId}...");

            _response = await _service.GetOrderByIdAsync(orderId);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when getting the order!");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        try
        {
            _logger.LogInformation($"Creating the order...");

            _response = await _service.CreateOrderAsync(request);

            return Created();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when creating the order!");
        }
    }

    [HttpDelete("{orderId:guid}")]
    public async Task<IActionResult> SoftDelete(Guid orderId)
    {
        try
        {
            _logger.LogInformation($"Deleting the order {orderId}...");
            
            _response = await _service.SoftDeleteOrderAsync(orderId);
            
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
            
            return BadRequest("Error(s) occurred when deleting the order!");
        }
    }
}
