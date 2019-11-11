using System;

namespace NGK_LAB10_WebAPI.Models
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        //Adding humidity and pressure
        public int Humidity { get; set; }

        public int AirPressure { get; set; }

        public string Summary { get; set; }
    }
}
