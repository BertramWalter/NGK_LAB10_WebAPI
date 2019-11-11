using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace NGK_LAB10_WebAPI.Models
{
    public class WeatherStation
    {
        public int WeatherStationId { get; set; }

        public DateTime Date { get; set; }

        [Column(TypeName = "decimal(10,1)")]
        public decimal TemperatureC { get; set; }

        public Location Location { get; set; }

        public int Humidity { get; set; }

        [Column(TypeName = "decimal(10,1)")]
        public decimal AirPressure { get; set; }
    }
}
