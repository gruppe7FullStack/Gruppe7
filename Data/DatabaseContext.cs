using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gruppe7.Models;

namespace Gruppe7.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext (DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Gruppe7.Models.WeatherForecast> WeatherForecast { get; set; } = default!;
        public DbSet<Gruppe7.Models.Observation> Observation { get; set; } = default!;
        public DbSet<Gruppe7.Models.RootObject> RootObject { get; set; } = default!;
        public DbSet<Gruppe7.Models.Level> Level { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfigurer Level som en keyless enhet
            modelBuilder.Entity<Level>().HasNoKey();
        }

    }
}
