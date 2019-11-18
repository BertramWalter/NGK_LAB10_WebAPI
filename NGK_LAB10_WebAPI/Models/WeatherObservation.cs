using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NGK_LAB10_WebAPI.Models
{
    public class WeatherObservation
    {
        public int WeatherObservationId { get; set; }

        public DateTime Date { get; set; }

        [Column(TypeName = "decimal(10,1)")]
        public decimal TemperatureC { get; set; }


        public Location Location { get; set; }

        public int Humidity { get; set; }

        [Column(TypeName = "decimal(10,1)")]
        public decimal AirPressure { get; set; }
    }
}
