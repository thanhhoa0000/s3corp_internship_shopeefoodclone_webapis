namespace ShopeeFoodClone.WebApi.Cart.Presentation.Configurations;

public static partial class AppExtensions
{
    public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(
                "CustomerOnly", 
                policy => policy.RequireClaim("Role", "Customer"));
        });

        return services;
    }
}