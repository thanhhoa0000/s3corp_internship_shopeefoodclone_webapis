namespace ShopeeFoodClone.WebApi.Cart.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICartService, CartService>();
        services.AddAutoMapper(typeof(CartMappingProfile));
        
        return services;
    }
}
