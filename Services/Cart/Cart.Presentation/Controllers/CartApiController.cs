namespace ShopeeFoodClone.WebApi.Cart.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/cart")]
    [ApiVersion(1)]
    public class CartApiController : ControllerBase
    {
        private readonly ICartService _service;
        private readonly ILogger<CartApiController> _logger;
        private Response _response;

        public CartApiController(ICartService service, ILogger<CartApiController> logger)
        {
            _service = service;
            _logger = logger;
            _response = new Response();
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetCart([FromRoute] Guid userId)
        {
            try
            {
                _logger.LogInformation($"Getting the cart of customer {userId}...");
                
                _response = await _service.GetCartAsync(customerId: userId);
                
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
                
                return BadRequest("Error(s) occurred when getting the cart!");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartDto cartDto)
        {
            try
            {
                _logger.LogInformation("Adding item to the cart...");
                
                _response = await _service.AddToCartAsync(cartDto);
                
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
                
                return BadRequest("Error(s) occurred when adding item to the cart!");
            }
        }

        [HttpDelete("item/{itemId:guid}")]
        public async Task<IActionResult> RemoveFromCart([FromRoute] Guid itemId)
        {
            try
            {
                _logger.LogInformation("Removing item from the cart...");
                
                _response = await _service.RemoveFromCartAsync(itemId);
                
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error(s) occurred: \n---\n{error}", ex);
                
                return BadRequest("Error(s) occurred when removing item from the cart!");
            }
        }
    }
}
