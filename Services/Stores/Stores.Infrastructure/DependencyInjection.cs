using ShopeeFoodClone.WebApi.Stores.Application.Interfaces;

namespace ShopeeFoodClone.WebApi.Stores.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StoreContext>(options
            => options.UseSqlServer(
                configuration.GetConnectionString("StoresDB"),
                sqlOptions => sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(15),
                    errorNumbersToAdd: null)
            ));
        
        services.AddScoped<IStoreRepository, StoreRepository>();
        services.AddScoped<IProvinceRepository, ProvinceRepository>();
        services.AddScoped<IDistrictRepository, DistrictRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();

        services.AddApplication();
        
        return services;
    }
}