namespace ShopeeFoodClone.WebApi.EventBus.RabbitMQ.Messaging;

public interface IRabbitMqSubscriber
{
    void Subscribe<T>(string queueName, Action<T> handler);
}
