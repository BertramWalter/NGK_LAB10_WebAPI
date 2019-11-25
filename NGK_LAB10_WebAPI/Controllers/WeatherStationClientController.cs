using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NGK_LAB10_WebAPI.Data;
using NGK_LAB10_WebAPI.Models;
using static BCrypt.Net.BCrypt;

namespace NGK_LAB10_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherStationClientController : ControllerBase
    {
        private const int BcryptWorkfactor = 11;
        private readonly AppDbContext _context;

        public WeatherStationClientController(AppDbContext context)
        {
            _context = context;
        }

        //// GET: api/WeatherStationClient
        [HttpPost("login"), AllowAnonymous]
        public async Task<ActionResult<TokenDto>> Login(LoginClient login)
        {
            login.SerialNumber = login.SerialNumber.ToLower();
            var user = await _context.WeatherStationClient.Where(u => u.SerialNumber == login.SerialNumber)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                var validPwd = Verify(login.Password, user.PwHash);
                if (validPwd)
                {
                    return new TokenDto {Token = GenerateToken(user.SerialNumber)};
                }
            }

            ModelState.AddModelError(string.Empty, "Forkert brugernavn eller password");
            return BadRequest(ModelState);
        }

        private string GenerateToken(string username)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            "the secret that needs to be at least 16 characters long for HmacSha256")),
                    SecurityAlgorithms.HmacSha256)), new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // GET: api/WeatherStationClient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherStationClient>>> GetWeatherStationClient()
        {
            return await _context.WeatherStationClient.ToListAsync();
        }

        // GET: api/WeatherStationClient/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherStationClient>> GetWeatherStationClient(long id)
        {
            var weatherStationClient = await _context.WeatherStationClient.FindAsync(id);

            if (weatherStationClient == null)
            {
                return NotFound();
            }

            return weatherStationClient;
        }

        // PUT: api/WeatherStationClient/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeatherStationClient(long id, WeatherStationClient weatherStationClient)
        {
            if (id != weatherStationClient.WeatherStationClientId)
            {
                return BadRequest();
            }

            _context.Entry(weatherStationClient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherStationClientExists(id))
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

        // POST: api/WeatherStationClient
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<WeatherStationClient>> PostWeatherStationClient(WeatherStationClient weatherStationClient)
        {
            _context.WeatherStationClient.Add(weatherStationClient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeatherStationClient", new { id = weatherStationClient.WeatherStationClientId }, weatherStationClient);
        }

        // DELETE: api/WeatherStationClient/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WeatherStationClient>> DeleteWeatherStationClient(long id)
        {
            var weatherStationClient = await _context.WeatherStationClient.FindAsync(id);
            if (weatherStationClient == null)
            {
                return NotFound();
            }

            _context.WeatherStationClient.Remove(weatherStationClient);
            await _context.SaveChangesAsync();
            
            return weatherStationClient;
        }

        private bool WeatherStationClientExists(long id)
        {
            return _context.WeatherStationClient.Any(e => e.WeatherStationClientId == id);
        }

        [HttpPost("Register"), AllowAnonymous]
        public async Task<ActionResult> Register(LoginClient c)
        {
            c.SerialNumber = c.SerialNumber.ToLower();
            var SerialNumberExists = await _context.WeatherStationClient
                .Where(c => c.SerialNumber == c.SerialNumber).FirstOrDefaultAsync();
            if (SerialNumberExists != null)
                return BadRequest(new {errorMessage = "Serial Number already registered"});

            WeatherStationClient client = new WeatherStationClient();

            client.SerialNumber = c.SerialNumber;
            client.PwHash = BCrypt.Net.BCrypt.HashPassword(c.Password, BcryptWorkfactor);

            _context.WeatherStationClient.Add(client);
            await _context.SaveChangesAsync();
            var jwtToken = new TokenDto();
            jwtToken.Token = GenerateToken(client.SerialNumber);
            return CreatedAtAction("Get", new {id = client.SerialNumber}, jwtToken);
        }
    }
}
