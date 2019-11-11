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
    public class WeatherStationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WeatherStationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/WeatherStation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherStation>>> GetWeatherStation()
        {
            return await _context.WeatherStation.ToListAsync();
        }


        // GET: api/WeatherStation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherStation>> GetWeatherStation(int id)
        {
            var weatherStation = await _context.WeatherStation.FindAsync(id);

            if (weatherStation == null)
            {
                return NotFound();
            }

            return weatherStation;
        }

        // PUT: api/WeatherStation/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeatherStation(int id, WeatherStation weatherStation)
        {
            if (id != weatherStation.WeatherStationId)
            {
                return BadRequest();
            }

            _context.Entry(weatherStation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherStationExists(id))
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

        // POST: api/WeatherStation
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<WeatherStation>> PostWeatherStation(WeatherStation weatherStation)
        {
            _context.WeatherStation.Add(weatherStation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeatherStation", new { id = weatherStation.WeatherStationId }, weatherStation);
        }

        // DELETE: api/WeatherStation/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WeatherStation>> DeleteWeatherStation(int id)
        {
            var weatherStation = await _context.WeatherStation.FindAsync(id);
            if (weatherStation == null)
            {
                return NotFound();
            }

            _context.WeatherStation.Remove(weatherStation);
            await _context.SaveChangesAsync();

            return weatherStation;
        }

        private bool WeatherStationExists(int id)
        {
            return _context.WeatherStation.Any(e => e.WeatherStationId == id);
        }
    }
}
