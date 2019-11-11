using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NGK_LAB10_WebAPI.Data;
using NGK_LAB10_WebAPI.Models;

namespace NGK_LAB10_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherObservationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WeatherObservationController(AppDbContext context)
        {
            _context = context;
        }

        //Via web.api’et kan andre klienter hente de seneste uploadede vejrdata
        [HttpGet]
        public IEnumerable<WeatherObservation> GetLatestWeatherData()
        {
            return Enumerable.Range(1, 4).Select(index => new WeatherObservation
            {
                Date = _context.WeatherObservation.Last().Date,
                TemperatureC = _context.WeatherObservation.Last().TemperatureC,
                Location = _context.WeatherObservation.Last().Location,
                Humidity = _context.WeatherObservation.Last().Humidity,
                AirPressure = _context.WeatherObservation.Last().AirPressure

            });
        }
        
        // GET: api/WeatherObservation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherObservation>>> GetWeatherObservation()
        {
            return await _context.WeatherObservation.ToListAsync();
        }

        // GET: api/WeatherObservation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherObservation>> GetWeatherObservation(int id)
        {
            var weatherObservation = await _context.WeatherObservation.FindAsync(id);

            if (weatherObservation == null)
            {
                return NotFound();
            }

            return weatherObservation;
        }

        // PUT: api/WeatherObservation/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
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
        [HttpPost]
        public async Task<ActionResult<WeatherObservation>> PostWeatherObservation(WeatherObservation weatherObservation)
        {
            _context.WeatherObservation.Add(weatherObservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeatherObservation", new { id = weatherObservation.WeatherObservationId }, weatherObservation);
        }

        // DELETE: api/WeatherObservation/5
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
