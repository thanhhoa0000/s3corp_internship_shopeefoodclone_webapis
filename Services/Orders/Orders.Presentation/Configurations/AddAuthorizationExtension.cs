namespace ShopeeFoodClone.WebApi.Orders.Presentation.Configurations;

public static partial class AppExtensions
{
    public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(
                "CustomerOnly", 
                policy => policy.RequireClaim("Role", "Customer"));
            
            options.AddPolicy(
                "VendorOnly", 
                policy => policy.RequireClaim("Role", "Vendor"));
            
            options.AddPolicy(
                "AdminOnly", 
                policy => policy.RequireClaim("Role", "Admin"));
        });

        return services;
    }
}