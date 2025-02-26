namespace ShopeeFoodClone.WebApi.Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityContext>(options
            => options.UseSqlServer(
                configuration.GetConnectionString("UsersDB"),
                sqlOptions => sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(15),
                    errorNumbersToAdd: null)
            ));
        
        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IIdentityRepository, IdentityRepository>();

        services.AddApplication();
        
        return services;
    }
}