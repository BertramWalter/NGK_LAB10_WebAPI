using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NGK_LAB10_WebAPI.Models
{
    public class WeatherStationClient
    {
        [Key]
        public long WeatherStationClientId { get; set; }
        
        [MaxLength(16)]
        public string SerialNumber { get; set; }
        
        [MaxLength(60)]
        public string PwHash { get; set; }
    }
}
