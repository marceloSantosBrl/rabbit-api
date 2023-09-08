namespace rabbit_api.Services;

public interface IMessageProducer
{
    void SendMessage<T>(T message);
}