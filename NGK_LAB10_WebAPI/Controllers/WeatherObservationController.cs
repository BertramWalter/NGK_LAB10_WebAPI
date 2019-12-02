using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NGK_LAB10_WebAPI.Data;
using NGK_LAB10_WebAPI.Hubs;
using NGK_LAB10_WebAPI.Models;

namespace NGK_LAB10_WebAPI.Controllers
{
    [Route("weather")]
    [ApiController]
    public class WeatherObservationController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<SubscribeHub> _hubContext;

        public WeatherObservationController(AppDbContext context, IHubContext<SubscribeHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        //Websocket Live Opdatering
        [HttpGet]

        //Via web.api’et kan andre klienter hente de seneste uploadede vejrdata
        [HttpGet("Latest")]
        public async Task<ActionResult<List<WeatherObservation>>> GetLatestWeatherData()
        {
            List<WeatherObservation> listWo = new List<WeatherObservation>();

            var count = _context.WeatherObservation.Count();

            if (count > 5)
            {
                for (int i = 0; i < 5; i++)
                {
                    WeatherObservation wo = new WeatherObservation
                    {
                        Date = _context.WeatherObservation.ElementAt(await _context.WeatherObservation.CountAsync() - i).Date,
                        TemperatureC = _context.WeatherObservation.ElementAt(await _context.WeatherObservation.CountAsync() - i).TemperatureC,
                        Location = _context.WeatherObservation.ElementAt(await _context.WeatherObservation.CountAsync() - i).Location,
                        Humidity = _context.WeatherObservation.ElementAt(await _context.WeatherObservation.CountAsync() - i).Humidity,
                        AirPressure = _context.WeatherObservation.ElementAt(await _context.WeatherObservation.CountAsync() - i).AirPressure
                    };
                    listWo.Add(wo);
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    WeatherObservation wo = new WeatherObservation
                    {
                        Date = _context.WeatherObservation.ElementAt(await _context.WeatherObservation.CountAsync() - i).Date,
                        TemperatureC = _context.WeatherObservation.ElementAt(await _context.WeatherObservation.CountAsync() - i).TemperatureC,
                        Location = _context.WeatherObservation.ElementAt(await _context.WeatherObservation.CountAsync() - i).Location,
                        Humidity = _context.WeatherObservation.ElementAt(await _context.WeatherObservation.CountAsync() - i).Humidity,
                        AirPressure = _context.WeatherObservation.ElementAt(await _context.WeatherObservation.CountAsync() - i).AirPressure
                    };
                    listWo.Add(wo);
                }
            }
            return listWo;
        }

        public async Task<ActionResult<IEnumerable<WeatherObservation>>> GetWeatherObservation()
        {
            return await _context.WeatherObservation.ToListAsync();
        }

        //Get data by temperature
        [HttpGet("{Date}")]
        public async Task<ActionResult<List<WeatherObservation>>> GetWeatherByDate(DateTime Date)
        {
            List<WeatherObservation> weatherObs = new List<WeatherObservation>();

            await foreach (var observation in _context.WeatherObservation)
            {
                if (Date == observation.Date)
                {
                    weatherObs.Add(observation);
                }
            }

            return weatherObs;
        }

        //Get weather data from specific time interval
        [HttpGet("{startTime,endTime}")]
        public async Task<ActionResult<List<WeatherObservation>>> GetWeatherObservationBetweenIntervals(DateTime startTime, DateTime endTime)
        {
            List<WeatherObservation> weatherObs = new List<WeatherObservation>();

            await foreach (var observation in _context.WeatherObservation)
            {
                if (observation.Date >= startTime && observation.Date <= endTime)
                {
                    weatherObs.Add(observation);
                }
            }

            return weatherObs;
        }

        // PUT: api/WeatherObservation/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeatherObservation(int id, WeatherObservation weatherObservation)
        {
            if (id != weatherObservation.WeatherObservationId)
            {
                return BadRequest();
            }

            _context.Entry(weatherObservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherObservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WeatherObservation
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<WeatherObservation>> PostWeatherObservation(WeatherObservation weatherObservation)
        {
            _context.WeatherObservation.Add(weatherObservation);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("WeatherUpdate", weatherObservation);

            return CreatedAtAction("GetWeatherObservation", new { id = weatherObservation.WeatherObservationId }, weatherObservation);
        }

        // DELETE: api/WeatherObservation/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<WeatherObservation>> DeleteWeatherObservation(int id)
        {
            var weatherObservation = await _context.WeatherObservation.FindAsync(id);
            if (weatherObservation == null)
            {
                return NotFound();
            }

            _context.WeatherObservation.Remove(weatherObservation);
            await _context.SaveChangesAsync();

            return weatherObservation;
        }

        private bool WeatherObservationExists(int id)
        {
            return _context.WeatherObservation.Any(e => e.WeatherObservationId == id);
        }
    }
}
