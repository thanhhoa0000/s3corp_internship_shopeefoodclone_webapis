namespace ShopeeFoodClone.WebApi.Payment.Presentation.Configurations;

public static partial class AppExtensions
{
    public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var iss = configuration.GetSection("JwtProperties:Issuer").Value;
                var aud = configuration.GetSection("JwtProperties:Audience").Value;
                var key = configuration.GetSection("JwtProperties:Key").Value;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = iss,
                    ValidAudience = aud,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key!))
                };
            });
        
        return services;
    }
}