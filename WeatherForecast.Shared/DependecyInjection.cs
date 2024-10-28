using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Shared.RabbitMQ;

namespace WeatherForecast.Shared
{
    public static class DependecyInjection
    {
        public static IServiceCollection RegisterSharedServices(this IServiceCollection services)
        {
            services.AddSingleton<IMessageConsumer, MessageConsumer>();
            services.AddSingleton<IMessagePublisher, MessagePublisher>();

            return services;
        }
    }
}
