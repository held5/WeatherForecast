using RabbitMQ.Client.Events;

namespace WeatherForecast.Shared.RabbitMQ
{
    public interface IMessageConsumer
    {
        void AddHandler(string queueName, EventHandler<BasicDeliverEventArgs> handler, bool autoAck);

        void Acknowledge(ulong deliveryTag);

        void RemoveHandler(string queueName, EventHandler<BasicDeliverEventArgs> handler);
    }
}
