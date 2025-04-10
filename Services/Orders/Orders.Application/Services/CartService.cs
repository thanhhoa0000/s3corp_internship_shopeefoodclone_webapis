namespace ShopeeFoodClone.WebApi.Orders.Application.Services;

public class CartService : ICartService
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;

    public CartService(IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        _configuration = configuration;
        _client = clientFactory.CreateClient("InternalShopeeFoodClone");
    }
    
    public async Task<bool> EmptyCart(Guid customerId)
    {
        var cartApiUrl = _configuration["ApiUrls:CartApi"];
        var responseFromClient = await _client.DeleteAsync($"{cartApiUrl}/api/v1/cart/empty-cart/{customerId}");

        if (responseFromClient.IsSuccessStatusCode)
        {
            return true;
        }
        
        return false;
    }
}
