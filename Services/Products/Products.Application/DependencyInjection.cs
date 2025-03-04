namespace ShopeeFoodClone.WebApi.Products.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddAutoMapper(typeof(ProductsMappingProfile));
        
        return services;
    }
}
