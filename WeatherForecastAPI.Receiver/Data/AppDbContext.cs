using Microsoft.EntityFrameworkCore;
using WeatherForecastAPI.Receiver.Models;

namespace WeatherForecastAPI.Receiver.Data
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Weather> Weathers { get; set; }
    }
}
