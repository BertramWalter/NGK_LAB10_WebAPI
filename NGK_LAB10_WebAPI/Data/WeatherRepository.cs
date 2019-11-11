using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using NGK_LAB10_WebAPI.Models;

namespace NGK_LAB10_WebAPI.Data
{
    public class WeatherRepository
    {
        public static WeatherRepository _instance = null;

        public List<WeatherForecast> _weatherForecasts = new List<WeatherForecast>();

        public static WeatherRepository GetInstance()
        {
            if (_instance != null)
            {
                return _instance;
            }
            _instance = new WeatherRepository();
            return _instance;
        }
    }
}
