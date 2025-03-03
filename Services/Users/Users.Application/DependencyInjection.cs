namespace ShopeeFoodClone.WebApi.Users.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserManagementService, UserManagementService>();
        services.AddScoped<IRoleManagementService, RoleManagementService>();
        services.AddAutoMapper(typeof(UsersMappingProfile));
        
        return services;
    }
}