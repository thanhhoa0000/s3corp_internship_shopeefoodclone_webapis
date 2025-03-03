namespace ShopeeFoodClone.WebApi.Users.Presentation.Configurations;

public static partial class AppExtensions
{
    public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Role", "Admin"));
        });

        return services;
    }
}