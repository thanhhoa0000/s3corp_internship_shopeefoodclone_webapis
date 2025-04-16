namespace ShopeeFoodClone.WebApi.Orders.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddHttpClient("InternalShopeeFoodClone_OrderToCart")
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
            });
        
        services
            .AddHttpClient("InternalShopeeFoodClone_OrderToProducts")
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
            });
        
        services
            .AddHttpClient("InternalShopeeFoodClone_OrderToStores")
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
            });
        
        services.AddHttpClient<ICartService, CartService>();
        services.AddHttpClient<IProductService, ProductService>();
        services.AddHttpClient<IStoreService, StoreService>();
        
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IStoreService, StoreService>();
        services.AddAutoMapper(typeof(OrdersMappingProfile));
        
        return services;
    }
}
