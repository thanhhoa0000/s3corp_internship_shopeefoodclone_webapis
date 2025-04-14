namespace ShopeeFoodClone.WebApi.Cart.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddHttpClient("InternalShopeeFoodClone_CartToProduct")
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
            });
        
        services.AddHttpClient<IProductService, ProductService>();

        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddAutoMapper(typeof(CartMappingProfile));
        
        return services;
    }
}
