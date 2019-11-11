using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NGK_LAB10_WebAPI.Models;
using NGK_LAB10_WebAPI.Data;

namespace NGK_LAB10_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //Post data from PostOffice
        [HttpPost]
        public void GetWeatherForecast(WeatherForecast wf)
        {
            WeatherRepository.GetInstance()._weatherForecasts.Add(wf);
        }

        //Get data by temperature
        [HttpGet("{TemperatureC}")]
        public List<WeatherForecast> GetDateWeather(int TemperatureC)
        {
            List<WeatherForecast> wf = new List<WeatherForecast>();

            foreach (var v in WeatherRepository.GetInstance()._weatherForecasts)
            {
                if (v.TemperatureC == TemperatureC)
                {
                    wf.Add(v);
                }
            }
            return wf;
        }



        //Standard Get - Get all weather data
        [HttpGet]
        public IEnumerable<WeatherForecast> GetAllWeatherData()
        {
            return WeatherRepository.GetInstance()._weatherForecasts;
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> GetLatestWeatherForecasts()
        //{
        //    return Enumerable.Range(1, 1).Select(index => new WeatherForecast
        //    {
        //        Date = WeatherRepository.GetInstance()._weatherForecasts.Last().Date,
        //        TemperatureC = WeatherRepository.GetInstance()._weatherForecasts.Last().TemperatureC,
        //        Humidity = WeatherRepository.GetInstance()._weatherForecasts.Last().Humidity,
        //        AirPressure = WeatherRepository.GetInstance()._weatherForecasts.Last().AirPressure,
        //        Summary = WeatherRepository.GetInstance()._weatherForecasts.Last().Summary
        //    })
        //        .ToArray();
        //}


        //public void GenerateWeatherData()
        //{
        //    var rng = new Random();

        //    WeatherForecast weatherForecast = new WeatherForecast();
        //    weatherForecast.Date = DateTime.Now.AddDays(4);
        //    weatherForecast.TemperatureC = rng.Next(-20, 55);
        //    weatherForecast.Humidity = rng.Next(7, 30);
        //    weatherForecast.AirPressure = rng.Next(1, 5);
        //    weatherForecast.Summary = Summaries[rng.Next(Summaries.Length)];

        //    WeatherRepository.GetInstance()._weatherForecasts.Add(weatherForecast);
        //}
    }
}
