using ShopeeFoodClone.WebApi.Users.Infrastructure.Persistence.Repositories;

namespace ShopeeFoodClone.WebApi.Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserContext>(options
            => options.UseSqlServer(
                configuration.GetConnectionString("UsersDB"),
                sqlOptions => sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(15),
                    errorNumbersToAdd: null)
            ));
        
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        services.AddApplication();
        
        return services;
    }
}