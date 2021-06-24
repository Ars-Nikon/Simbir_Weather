using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Models
{
    public class CityContext : DbContext
    {
        public DbSet<City> Cities { get; set; }

        public CityContext(DbContextOptions<CityContext> options)
                 : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
