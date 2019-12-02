using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NGK_LAB10_WebAPI.Controllers;
using NGK_LAB10_WebAPI.Data;
using NGK_LAB10_WebAPI.Models;
using NUnit.Framework;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace Unit.Test.Controllers
{
    public class UnitTests
    {
        private readonly AppDbContext _context;
        private WeatherObservationController _uut;
        private List<WeatherObservation> _listOfWeatherObservations;
        private Location[] _locationAr;
        

        public UnitTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _context.Database.EnsureCreated();

            _uut = new WeatherObservationController(_context);

            _listOfWeatherObservations = new List<WeatherObservation>();

            _locationAr = new Location[10];

            _locationAr[0] = new Location { Latitude = 100, Longitude = 100, Name = "Horsens" };
            _locationAr[1] = new Location { Latitude = 200, Longitude = 200, Name = "Aarhus" };
            _locationAr[2] = new Location { Latitude = 300, Longitude = 300, Name = "Herning" };
            _locationAr[3] = new Location { Latitude = 400, Longitude = 400, Name = "Hedensted" };
            _locationAr[4] = new Location { Latitude = 500, Longitude = 500, Name = "Randers" };
            _locationAr[5] = new Location { Latitude = 600, Longitude = 600, Name = "Koebenhavn" };
            _locationAr[6] = new Location { Latitude = 700, Longitude = 700, Name = "Haderslev" };
            _locationAr[7] = new Location { Latitude = 800, Longitude = 800, Name = "Viborg" };
            _locationAr[8] = new Location { Latitude = 900, Longitude = 900, Name = "Skive" };
            _locationAr[9] = new Location { Latitude = 1000, Longitude = 1000, Name = "Aalborg" };

            for (int i = 0; i < 10; i++)
            {
                var wo = new WeatherObservation
                {
                    AirPressure = i,
                    Date = DateTime.Now,
                    Humidity = i + 5,
                    Location = _locationAr[i],
                    TemperatureC = i + 10,
                    WeatherObservationId = i+100
                };
                _listOfWeatherObservations.Add(wo);
            }
        }

        [Fact]
        public async void GetLatestWeatherData_10DocumentsAvailable_ReturnLatest5()
        {
            UnitTests u = new UnitTests();

            foreach (var w in _listOfWeatherObservations)
            {
                await _uut.PostWeatherObservation(w);
            }

            ActionResult<List<WeatherObservation>> lastFiveWo = new List<WeatherObservation>();

            lastFiveWo = await _uut.GetLatestWeatherData();

            Assert.That(lastFiveWo, Is.EqualTo(_locationAr));
        }

        [Fact]
        public async void GetWeatherObservation_10Added_DataCorrectReturned()
        {
            UnitTests u = new UnitTests();

            foreach (var w in _listOfWeatherObservations)
            {
                await _uut.PostWeatherObservation(w);
            }

            ActionResult<IEnumerable<WeatherObservation>> weatherData = new List<WeatherObservation>();

            weatherData = await _uut.GetWeatherObservation();

            var wdInDb = JsonConvert.SerializeObject(weatherData);
            var wdInList = JsonConvert.SerializeObject(_listOfWeatherObservations);

            string s = "{\"Result\":null,\"Value\":";

            Assert.AreEqual(wdInDb, s + wdInList + "}");
        }

        //[Fact]
        //public async void GetWeatherObservationBetweenIntervals_FindBetween8And9_DataCorrectReturned()
        //{
        //    UnitTests u = new UnitTests();

        //    foreach (var w in _listOfWeatherObservations)
        //    {
        //        w.Date = new DateTime(02 - 12 - 12 - 30);
        //        await _uut.PostWeatherObservation(w);
        //    }

        //    ActionResult<List<WeatherObservation>> weatherData = new List<WeatherObservation>();

        //    DateTime start = new DateTime(0210);
        //    DateTime end = new DateTime(02 - 12 - 13 - 00);

        //    weatherData = await _uut.GetWeatherObservationBetweenIntervals(start, end);

        //    var wdInDb = JsonConvert.SerializeObject(weatherData);
        //    var wdInList = JsonConvert.SerializeObject(_listOfWeatherObservations);

        //    //string s = "{\"Result\":null,\"Value\":";

        //    Assert.AreEqual(wdInDb,  wdInList);
        //}
    }
}
