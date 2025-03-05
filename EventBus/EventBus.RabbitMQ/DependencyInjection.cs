namespace ShopeeFoodClone.WebApi.EventBus.RabbitMQ;

public static class DependencyInjection
{
    public static IServiceCollection AddRabbitMqServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqOptions>(configuration.GetSection("EventBus"));
        
        services.AddSingleton<IRabbitMqPublisher, RabbitMqMessageBus>();
        services.AddSingleton<IRabbitMqSubscriber, RabbitMqMessageBus>();

        return services;
    }
}
