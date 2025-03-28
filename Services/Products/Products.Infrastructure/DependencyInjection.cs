namespace ShopeeFoodClone.WebApi.Products.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProductContext>(options
            => options.UseSqlServer(
                configuration.GetConnectionString("ProductsDB"),
                sqlOptions => sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(15),
                    errorNumbersToAdd: null)
            ));
        
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IMenuRepository, MenuRepository>();

        services.AddApplication();
        
        return services;
    }
}