namespace ShopeeFoodClone.WebApi.Identity.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddAutoMapper(typeof(IdentityMappingProfile));
        
        return services;
    }
}