namespace ShopeeFoodClone.WebApi.Identity.Presentation.Configurations;

public static partial class AppExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();
        
        return services;
    }
}