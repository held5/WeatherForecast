using RabbitMQ.Client;

namespace WeatherForecast.Shared.RabbitMQ
{
    internal class MessagePublisher : MessageBase, IMessagePublisher
    {
        public MessagePublisher(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public void PublishMessage(string queueName, byte[] message)
        {
            try
            {
                CreateQueue(queueName);

                Channel.BasicPublish(
                    "",
                    queueName,
                    basicProperties: null,
                    mandatory: true,
                    body: message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
