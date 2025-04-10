namespace ShopeeFoodClone.WebApi.Orders.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        
        services.AddHttpClient<ICartService, CartService>();
        
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICartService, CartService>();
        services.AddAutoMapper(typeof(OrdersMappingProfile));
        
        return services;
    }
}
