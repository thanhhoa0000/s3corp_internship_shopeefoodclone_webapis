namespace ShopeeFoodClone.WebApi.Stores.Presentation.Configurations;

public static partial class AppExtensions
{
    public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(
                "AdminOnly", 
                policy => policy.RequireClaim("Role", "Admin"));
            
            options.AddPolicy(
                "VendorOnly",
                policy => policy.RequireClaim("Role", "Vendor"));
            
            options.AddPolicy(
                "AdminAndVendorOnly",
                policy => policy.RequireClaim("Role", "Admin", "Vendor"));
        });

        return services;
    }
}