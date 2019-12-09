using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NGK_LAB10_WebAPI.Controllers;
using NGK_LAB10_WebAPI.Data;
using NGK_LAB10_WebAPI.Hubs;
using NGK_LAB10_WebAPI.Models;
using NSubstitute;
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
        //IHubContext
        private readonly IHubContext<SubscribeHub> _fakeHubContext;
        
        public UnitTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _context.Database.EnsureCreated();

            //IHubContext
            _fakeHubContext = Substitute.For<IHubContext<SubscribeHub>>();


            _uut = new WeatherObservationController(_context, _fakeHubContext);

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

        #region GetWeatherObservations

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

        #endregion

        #region PostWeatherObservation

        //[Fact]
        //public async void PosttWeatherData_1ObservationInserted_CorrectInserted()
        //{
        //    //Change count < 1 to check for more insertions
        //    UnitTests u = new UnitTests();

        //    int count = 0;
        //    foreach (var w in _listOfWeatherObservations)
        //    {
        //        if (count < 1)
        //        {
        //            await _uut.PostWeatherObservation(w);
        //        }
        //        else
        //        {
        //            break;
        //        }

        //        count++;
        //    }

        //    ActionResult<IEnumerable<WeatherObservation>> PostedData = new List<WeatherObservation>();

        //    PostedData = await _uut.GetWeatherObservation();

        //    Assert.That(PostedData.Value.GetEnumerator().Current.Location.Name, Is.EqualTo("Horsens"));
        //}



        //[Fact]
        //public async void PosttWeatherData_Posted10Observations_NumberOfObservationsIs10()
        //{
        //    UnitTests u = new UnitTests();

        //    foreach (var w in _listOfWeatherObservations)
        //    {
        //        await _uut.PostWeatherObservation(w);
        //    }

        //    ActionResult<IEnumerable<WeatherObservation>> PostedData = new List<WeatherObservation>();

        //    PostedData = await _uut.GetWeatherObservation();

        //    Assert.That(PostedData.Value.Count, Is.EqualTo(5));
        //}


        #endregion

        #region GetLatestWeatherData

        [Fact]
        public async void GetLatestWeatherData_10DocumentsAvailable_Return5()
        {
            UnitTests u = new UnitTests();

            foreach (var w in _listOfWeatherObservations)
            {
                await _uut.PostWeatherObservation(w);
            }

            ActionResult<List<WeatherObservation>> lastFiveWo = new List<WeatherObservation>();

            lastFiveWo = await _uut.GetLatestWeatherData();

            Assert.That(lastFiveWo.Value.Count, Is.EqualTo(5));
        }


        [Fact]
        public async void GetLatestWeatherData_10DocumentsAvailable_ReturnCorrect5()
        {
            UnitTests u = new UnitTests();

            foreach (var w in _listOfWeatherObservations)
            {
                await _uut.PostWeatherObservation(w);
            }

            ActionResult<List<WeatherObservation>> lastFiveWo = new List<WeatherObservation>();

            lastFiveWo = await _uut.GetLatestWeatherData();

            var wdInDb = JsonConvert.SerializeObject(lastFiveWo);

            Assert.AreEqual(lastFiveWo.Value.ElementAt(0).Location.Name, "Aalborg");
            Assert.AreEqual(lastFiveWo.Value.ElementAt(1).Location.Name, "Skive");
            Assert.AreEqual(lastFiveWo.Value.ElementAt(2).Location.Name, "Viborg");
            Assert.AreEqual(lastFiveWo.Value.ElementAt(3).Location.Name, "Haderslev");
            Assert.AreEqual(lastFiveWo.Value.ElementAt(4).Location.Name, "Koebenhavn");
        }


        #endregion

        #region GetWeatherObservationBetweenIntervals

        [Fact]
        public async void GetWeatherObservationBetweenIntervals_FindBetween8And9_AllDataIsBetweenInterval()
        {
            UnitTests u = new UnitTests();

            foreach (var w in _listOfWeatherObservations)
            {
                w.Date = new DateTime(2019, 12, 02, 08, 30, 00);
                await _uut.PostWeatherObservation(w);
            }

            ActionResult<List<WeatherObservation>> weatherData = new List<WeatherObservation>();

            DateTime start = new DateTime(2019, 12, 02, 08, 00, 00);
            DateTime end = new DateTime(2019, 12, 02, 09, 00, 00);

            weatherData = await _uut.GetWeatherObservationBetweenIntervals(start, end);

            var wdInDb = JsonConvert.SerializeObject(weatherData);
            var wdInList = JsonConvert.SerializeObject(_listOfWeatherObservations);

            string s = "{\"Result\":null,\"Value\":";

            Assert.AreEqual(wdInDb, s + wdInList + "}");
        }

        [Fact]
        public async void GetWeatherObservationBetweenIntervals_FindBetween8And9_NoDataIsBetweenInterval()
        {
            UnitTests u = new UnitTests();

            foreach (var w in _listOfWeatherObservations)
            {
                w.Date = new DateTime(2019, 12, 02, 09, 30, 00);
                await _uut.PostWeatherObservation(w);
            }

            ActionResult<List<WeatherObservation>> weatherData = new List<WeatherObservation>();

            DateTime start = new DateTime(2019, 12, 02, 08, 00, 00);
            DateTime end = new DateTime(2019, 12, 02, 09, 00, 00);

            weatherData = await _uut.GetWeatherObservationBetweenIntervals(start, end);

            var wdInDb = JsonConvert.SerializeObject(weatherData);
            var wdInList = JsonConvert.SerializeObject(_listOfWeatherObservations);

            string s = "{\"Result\":null,\"Value\":";

            Assert.AreNotEqual(wdInDb, s + wdInList + "}");
        }


        [Fact]
        public async void GetWeatherObservationBetweenIntervals_FindBetween8And9_5ObservationsIsBetweenInterval()
        {
            UnitTests u = new UnitTests();

            int count = 0;
            foreach (var w in _listOfWeatherObservations)
            {
                if (count < 5)
                {
                    w.Date = new DateTime(2019, 12, 02, 08, 30, 00);
                    await _uut.PostWeatherObservation(w);
                }
                else
                {
                    w.Date = new DateTime(2019, 12, 02, 10, 30, 00);
                    await _uut.PostWeatherObservation(w);
                }
                count++;
            }

            ActionResult<List<WeatherObservation>> weatherData = new List<WeatherObservation>();

            DateTime start = new DateTime(2019, 12, 02, 08, 00, 00);
            DateTime end = new DateTime(2019, 12, 02, 09, 00, 00);

            weatherData = await _uut.GetWeatherObservationBetweenIntervals(start, end);

            Assert.That(weatherData.Value.Count, Is.EqualTo(5));
        }

        #endregion

        #region GetWeatherObservationByDate

        [Fact]
        public async void GetWeatherObservationByDate_Date201911202_Return1CorrectObservation()
        {
            UnitTests u = new UnitTests();
            int count = 0;
            foreach (var w in _listOfWeatherObservations)
            {
                if (count < 1)
                {
                    w.Date = new DateTime(2019, 12, 02, 08, 30, 00);
                    await _uut.PostWeatherObservation(w);
                }
                else
                {
                    w.Date = new DateTime(2019, 12, 05, 10, 30, 00);
                    await _uut.PostWeatherObservation(w);
                }
                count++;
            }

            ActionResult<List<WeatherObservation>> weatherData = new List<WeatherObservation>();

            DateTime getDate = new DateTime(2019, 12, 02, 08, 30, 00);

            weatherData = await _uut.GetWeatherByDate(getDate);

            Assert.That(weatherData.Value.Count, Is.EqualTo(1));
        }

        [Fact]
        public async void GetWeatherObservationByDate_Date201911202_Return0CorrectObservations()
        {
            UnitTests u = new UnitTests();
            int count = 0;
            foreach (var w in _listOfWeatherObservations)
            {
                if (count < 1)
                {
                    w.Date = new DateTime(2019, 12, 02, 08, 30, 00);
                    await _uut.PostWeatherObservation(w);
                }
                else
                {
                    w.Date = new DateTime(2019, 12, 05, 10, 30, 00);
                    await _uut.PostWeatherObservation(w);
                }
                count++;
            }

            ActionResult<List<WeatherObservation>> weatherData = new List<WeatherObservation>();

            DateTime getDate = new DateTime(2020, 12, 02, 08, 30, 00);

            weatherData = await _uut.GetWeatherByDate(getDate);

            Assert.AreNotEqual(1, weatherData.Value.Count);
        }


        #endregion
    }
}
