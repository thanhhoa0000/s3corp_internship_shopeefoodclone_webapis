using Microsoft.Extensions.Configuration;

namespace ShopeeFoodClone.WebApi.Cart.Application.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;

    public ProductService(IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        _configuration = configuration;
        _client = clientFactory.CreateClient("InternalShopeeFoodClone_CartToProduct");
    }

    public async Task<Response?> GetProductAsync(Guid productId)
    {
        var productsApiUrl = _configuration["ApiUrls:ProductsApi"];
        var responseFromClient = await _client.GetAsync($"{productsApiUrl}/api/v1/products/{productId}");
        
        var content = await responseFromClient.Content.ReadAsStringAsync();

        var response = new Response();
        response = JsonSerializer.Deserialize<Response>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return response;
    }
}
