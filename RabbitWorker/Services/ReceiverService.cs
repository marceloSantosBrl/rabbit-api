using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitWorker.Services;

public class ReceiverService: IReceiverService
{
    public void PrintQueueMessage()
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
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(message);
        };
        
        channel.BasicConsume(
            queue: "test",
            autoAck: true,
            consumer: consumer
            );
    }
}