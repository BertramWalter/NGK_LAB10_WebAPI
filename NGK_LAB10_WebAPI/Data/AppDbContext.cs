using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NGK_LAB10_WebAPI.Data
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=Localhost;Initial Catalog=NGK3;Integrated Security=True");
        }

        //DbSets here:
        //public DbSet<Model/tablename> Table { get; set; }
    }
}
