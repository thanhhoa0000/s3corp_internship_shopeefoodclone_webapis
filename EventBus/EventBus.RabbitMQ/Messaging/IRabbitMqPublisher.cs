namespace ShopeeFoodClone.WebApi.EventBus.RabbitMQ.Messaging;

public interface IRabbitMqPublisher
{
    void Publish<T>(T message, string queueName);
}
