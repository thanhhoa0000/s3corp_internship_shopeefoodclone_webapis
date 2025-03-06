namespace ShopeeFoodClone.WebApi.Orders.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();
        services.AddAutoMapper(typeof(OrdersMappingProfile));
        
        return services;
    }
}
