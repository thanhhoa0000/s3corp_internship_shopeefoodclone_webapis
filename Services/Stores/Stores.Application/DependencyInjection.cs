namespace ShopeeFoodClone.WebApi.Stores.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IStoreService, StoreService>();
        services.AddScoped<IProvinceService, ProvinceService>();
        services.AddScoped<IDistrictService, DistrictService>();
        services.AddScoped<IWardService, WardService>();
        services.AddScoped<ICollectionService, CollectionService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ISubCategoryService, SubCategoryService>();
        services.AddAutoMapper(typeof(StoresMappingProfile));
        
        return services;
    }
}