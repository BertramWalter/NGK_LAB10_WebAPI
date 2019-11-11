using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace NGK_LAB10_WebAPI.Models
{
    public class WeatherStation
    {

        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }

        public Location Location { get; set; }

        public int Humidity { get; set; }

        public int AirPressure { get; set; }
    }
}
