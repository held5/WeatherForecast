using RabbitMQ.Client;
using WeatherForecast.Shared;
using WeatherForecastAPI.Sender.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add rabbitmq factory
builder.Services.AddSingleton<IConnectionFactory>(sp =>
{
    var rabbitMqConfig = builder.Configuration.GetSection("RabbitMq");

    var factory = new ConnectionFactory()
    {
        HostName = rabbitMqConfig["HostName"],
        Port = int.Parse(rabbitMqConfig["Port"])
    };
    return factory;
});

builder.Services.RegisterSharedServices();

// Add bg service
builder.Services.AddHostedService<PublisherService>();

builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//HealthCheck Middleware
app.MapHealthChecks("/api/health");

app.Run();
