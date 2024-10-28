using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using WeatherForecast.Shared;
using WeatherForecastAPI.Receiver.Data;
using WeatherForecastAPI.Receiver.Services;

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

// Add pg db
builder.Services.AddDbContext<AppDbContext>(o =>o.UseNpgsql(builder.Configuration.GetConnectionString("Db")));

builder.Services.RegisterSharedServices();

// Add Bg service
builder.Services.AddHostedService<ConsumerService>();

builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Automatically apply pending migrations
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//HealthCheck Middleware
app.MapHealthChecks("/api/health");

app.Run();
