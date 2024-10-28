using Newtonsoft.Json;
using System.Text;
using WeatherForecast.Shared.Models;
using WeatherForecast.Shared.RabbitMQ;

namespace WeatherForecastAPI.Sender.Services
{
    internal class PublisherService : BackgroundService
    {
        private readonly IMessagePublisher _messagePublisher;
        private readonly ILogger _logger;

        private static readonly string[] Summaries = new[]
{
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public PublisherService(ILogger<PublisherService> logger, IMessagePublisher messagePublisher)
        {
            _logger = logger;
            _messagePublisher = messagePublisher;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var weatherModel = new WeatherForecastDto(DateOnly.FromDateTime(DateTime.Now), Random.Shared.Next(-10, 45), Summaries[Random.Shared.Next(Summaries.Length)]);

                var messageJson = JsonConvert.SerializeObject(weatherModel);
                var body = Encoding.UTF8.GetBytes(messageJson);

                _logger.LogInformation("SENDING new weather forecast => Date: {0}, TempC: {1}, Summary: {2}",
                    weatherModel.Date, weatherModel.TemperatureC, weatherModel.Summary);

                _messagePublisher.PublishMessage(MessageQueues.WeatherForecastQueue, body);

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
