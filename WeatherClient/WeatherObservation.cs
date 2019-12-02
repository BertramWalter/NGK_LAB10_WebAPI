using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherClient
{
    public class WeatherObservation
    {
        public DateTime Date { get; set; }

        public decimal TemperatureC { get; set; }

        public int Humidity { get; set; }

        public decimal AirPressure { get; set; }
    }
}
