using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace rabbit_api.Services;

public class MessageProducer : IMessageProducer
{
    public void SendMessage<T>(T message)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(
            "test",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish(exchange: "", routingKey: "test", body: body);
    }
}