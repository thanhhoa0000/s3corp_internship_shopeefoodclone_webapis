namespace ShopeeFoodClone.WebApi.Payment.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPaymentService, PaymentService>();
        
        services.AddAutoMapper(typeof(PaymentMappingProfile));
        
        return services;
    }
}
