namespace ShopeeFoodClone.WebApi.Stores.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IStoreService, StoreService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddAutoMapper(typeof(StoresMappingProfile));
        
        return services;
    }
}