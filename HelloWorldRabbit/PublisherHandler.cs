using RabbitMQ.Client;

namespace HelloWorldRabbit;
public static class PublisherHandler
{
    public static void Handle(string hostname, string queue, string routingKey, byte[] body)
    {
        var factory = new ConnectionFactory { HostName = hostname };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();


        channel.QueueDeclare(queue: queue,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        channel.BasicPublish(exchange: string.Empty,
                     routingKey: routingKey,
                     basicProperties: null,
                     body: body);
    }
}
