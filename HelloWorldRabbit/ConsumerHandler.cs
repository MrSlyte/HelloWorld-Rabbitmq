using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HelloWorldRabbit;
public class ConsumerHandler : IDisposable
{
    private IConnection? _connection;
    private IModel? _channel;
    private bool _disposed;

    public IModel? Channel { get { return _channel; } }

    public void CreateConnection(string hostname)
    {
        if (_connection == null)
        {
            var factory = new ConnectionFactory { HostName = hostname };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }
    }

    public void Handle(string hostname, string queue, bool autoAct, EventHandler<BasicDeliverEventArgs> received)
    {
        CreateConnection(hostname);

        _channel?.QueueDeclare(queue: queue,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += received;
        _channel?.BasicConsume(queue: queue,
                             autoAck: autoAct,
                             consumer: consumer);
    }

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                if (_connection != null)
                {
                    _connection.Dispose();
                }
                if (_channel != null)
                {
                    _channel.Dispose();
                }

                _connection = null;
                _channel = null;
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}