using RabbitMQ.Client;

namespace WeatherForecast.Shared.RabbitMQ
{
    internal class MessageBase : IDisposable
    {
        private readonly IConnection _connection;
        protected readonly IModel Channel;

        public MessageBase(IConnectionFactory connectionFactory)
        {
            _connection = connectionFactory.CreateConnection();
            Channel = _connection.CreateModel();
        }

        /// <summary>
        ///     Creates a queue if not exists.
        /// </summary>
        /// <param name="queueName"></param>
        protected void CreateQueue(string queueName)
            => Channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        public void Dispose()
        {
            _connection?.Dispose();
            Channel?.Dispose();
        }
    }
}
