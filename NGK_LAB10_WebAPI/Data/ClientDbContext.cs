using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NGK_LAB10_WebAPI.Data
{
    public class ClientDbContext : IdentityDbContext
    {
        public ClientDbContext(DbContextOptions<ClientDbContext>
            options) :base(options)
        { }
    }
}
