namespace ShopeeFoodClone.WebApi.Payment.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PaymentContext>(options
            => options.UseSqlServer(
                configuration.GetConnectionString("PaymentDB"),
                sqlOptions => sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(15),
                    errorNumbersToAdd: null)
            ));
        
        services.AddScoped<IPaymentRepository, PaymentRepository>();

        services.AddApplication();
        
        return services;
    }
}
