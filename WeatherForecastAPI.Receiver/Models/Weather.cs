using System.ComponentModel.DataAnnotations;

namespace WeatherForecastAPI.Receiver.Models
{
    public class Weather
    {
        [Key]
        public Guid Id { get; set; }

        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public string? Summary { get; set; }
    }
}
