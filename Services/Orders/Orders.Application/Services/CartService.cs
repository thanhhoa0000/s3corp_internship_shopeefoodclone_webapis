namespace ShopeeFoodClone.WebApi.Orders.Application.Services;

public class CartService : ICartService
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _accessor;

    public CartService(
        IHttpClientFactory clientFactory, 
        IConfiguration configuration, 
        IHttpContextAccessor accessor)
    {
        _configuration = configuration;
        _accessor = accessor;
        _client = clientFactory.CreateClient("InternalShopeeFoodClone_OrderToCart");
    }
    
    public async Task<bool> EmptyCart(Guid customerId)
    {
        var accessToken = _accessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);
        
        var cartApiUrl = _configuration["ApiUrls:CartApi"];
        var responseFromClient = await _client.DeleteAsync($"{cartApiUrl}/api/v1/cart/empty-cart/{customerId}");

        if (responseFromClient.IsSuccessStatusCode)
        {
            return true;
        }
        
        return false;
    }
}
