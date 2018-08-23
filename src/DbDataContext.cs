using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODataMicroservice
{
    public class DbDataContext : DbContext
    {
        public DbDataContext(DbContextOptions<DbDataContext> options) : base(options)
        {
        }

        public DbSet<TemperatureReading> TemperatureReadings { get; set; }

        public DbSet<Alert> Alerts { get; set; }
    }
}
