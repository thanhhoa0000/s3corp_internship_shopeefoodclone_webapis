namespace ShopeeFoodClone.WebApi.Orders.Application.Services;

public class StoreService : IStoreService
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;

    public StoreService(IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        _configuration = configuration;
        _client = clientFactory.CreateClient("InternalShopeeFoodClone_OrderToStores");
    }
    
    public async Task<Response?> GetStoreNameAsync(Guid storeId)
    {
        var storesApiUrl = _configuration["ApiUrls:StoresApi"];
        var responseFromClient = await _client.GetAsync($"{storesApiUrl}/api/v1/stores/get-name/{storeId}");
        
        var content = await responseFromClient.Content.ReadAsStringAsync();

        var response = new Response();
        response = JsonSerializer.Deserialize<Response>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return response;
    }
}
