using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace WeatherForecast.Shared.RabbitMQ
{
    internal class MessageConsumer : MessageBase, IMessageConsumer
    {
        private readonly Dictionary<string, EventingBasicConsumer> _consumers;

        public MessageConsumer(IConnectionFactory connectionFactory): base(connectionFactory)
        {
            _consumers = new Dictionary<string, EventingBasicConsumer>();
        }

        public void AddHandler(string queueName, EventHandler<BasicDeliverEventArgs> handler, bool autoAck)
        {
            try
            {
                if (_consumers.ContainsKey(queueName))
                {
                    throw new InvalidOperationException($"A consumer for the queue '{queueName}' is already registered.");
                }

                var consumer = new EventingBasicConsumer(Channel);
                consumer.Received += handler;

                CreateQueue(queueName);

                Channel.BasicConsume(
                    queue: queueName,
                    autoAck: autoAck,
                    consumer: consumer,
                    noLocal: false,
                    exclusive: false,
                    consumerTag: Guid.NewGuid().ToString(),
                    arguments: new Dictionary<string, object>()
                );

                _consumers.Add(queueName, consumer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveHandler(string queueName, EventHandler<BasicDeliverEventArgs> handler)
        {
            if (_consumers.TryGetValue(queueName, out var consumer))
            {
                try
                {
                    consumer.Received -= handler;

                    _consumers.Remove(queueName);
                }
                catch (Exception) 
                {
                    throw;
                }
            }
        }

        public void Acknowledge(ulong deliveryTag) => Channel.BasicAck(deliveryTag: deliveryTag, multiple: false);
    }
}
