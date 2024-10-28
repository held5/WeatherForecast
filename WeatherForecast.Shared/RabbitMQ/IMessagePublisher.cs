namespace WeatherForecast.Shared.RabbitMQ
{
    public interface IMessagePublisher
    {
        void PublishMessage(string queueName, byte[] message);
    }
}
