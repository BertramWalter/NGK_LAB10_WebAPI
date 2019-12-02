using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NGK_LAB10_WebAPI.Models;

namespace NGK_LAB10_WebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<NGK_LAB10_WebAPI.Models.WeatherObservation> WeatherObservation { get; set; }
        public DbSet<NGK_LAB10_WebAPI.Models.WeatherStationClient> WeatherStationClient { get; set; }

        //DbSets here:
        //public DbSet<Location> Locations { get; set; }
        //public DbSet<WeatherObservation> WeatherObservations { get; set; }
        //public DbSet<WeatherStationClient> WeatherStationClients { get; set; }
    }
}
