namespace ShopeeFoodClone.WebApi.Products.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IMenuService, MenuService>();
        services.AddAutoMapper(typeof(ProductsMappingProfile));
        
        return services;
    }
}
