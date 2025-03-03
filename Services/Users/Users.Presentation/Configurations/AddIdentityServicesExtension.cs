namespace ShopeeFoodClone.WebApi.Users.Presentation.Configurations;

public static partial class AppExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<UserContext>()
            .AddDefaultTokenProviders();
        
        return services;
    }
}