namespace ShopeeFoodClone.WebApi.EventBus.RabbitMQ.Messaging;

public class RabbitMqMessageBus : IRabbitMqPublisher, IRabbitMqSubscriber
{
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    public RabbitMqMessageBus(IConfiguration configuration)
    {
        var factory = new ConnectionFactory()
        {
            HostName = configuration["EventBus:HostName"]!,
            UserName = configuration["EventBus:Username"]!,
            Password = configuration["EventBus:Password"]!,
            Port = int.Parse(configuration["EventBus:Port"]!)
        };
        
        _connection = factory.CreateConnectionAsync().Result;
        _channel = _connection.CreateChannelAsync().Result;
    }

    public void Publish<T>(T message, string queueName)
    {
        _channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false);
        var jsonMessage = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonMessage);
        _channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, body: body);
    }
    
    public void Subscribe<T>(string queueName, Action<T> handler)
    {
        _channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false);
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += (sender, args) =>
        {
            var jsonMessage = Encoding.UTF8.GetString(args.Body.ToArray());
            var message = JsonSerializer.Deserialize<T>(jsonMessage);
            handler(message!);
            return Task.CompletedTask;
        };
        _channel.BasicConsumeAsync(queueName, autoAck: true, consumer);
    }
}
