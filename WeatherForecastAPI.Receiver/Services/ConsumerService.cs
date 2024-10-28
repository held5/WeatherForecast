using System.Text;
using System.Text.Json;
using WeatherForecast.Shared.Models;
using WeatherForecast.Shared.RabbitMQ;
using WeatherForecastAPI.Receiver.Data;
using WeatherForecastAPI.Receiver.Models;

namespace WeatherForecastAPI.Receiver.Services
{
    internal class ConsumerService : BackgroundService
    {
        private readonly ILogger<ConsumerService> _logger;
        private readonly IMessageConsumer _messageConsumer;
        private readonly IServiceProvider _serviceProvider;

        public ConsumerService(ILogger<ConsumerService> logger, IMessageConsumer messageConsumer, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _messageConsumer = messageConsumer;
            _serviceProvider = serviceProvider;
        }
         
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageConsumer.AddHandler(MessageQueues.WeatherForecastQueue, async (_, args) =>
            {
                try
                {
                    var jsonBody = Encoding.UTF8.GetString(args.Body.ToArray());
                    var weatherForecast = JsonSerializer.Deserialize<WeatherForecastDto>(jsonBody)!;

                    _logger.LogInformation("RECEIVED new weather forecast => Date: {0}, TempC: {1}, Summary: {2}",
                        weatherForecast.Date, weatherForecast.TemperatureC, weatherForecast.Summary);

                    var newWeatherForecast = new Weather
                    {
                        Date = weatherForecast.Date,
                        Summary = weatherForecast.Summary,
                        TemperatureC = weatherForecast.TemperatureC,
                    };

                    using var scope = _serviceProvider.CreateScope();
                    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    await ctx.Weathers.AddAsync(newWeatherForecast);

                    int affectedRows = await ctx.SaveChangesAsync();

                    if (affectedRows > 0)
                    {
                        _logger.LogInformation("Weather forecast saved successfully.");
                        _messageConsumer.Acknowledge(args.DeliveryTag);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error ocurred while receiving and processing a weather report");
                }
            }, false);

            return Task.CompletedTask;
        }
    }
}
