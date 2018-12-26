using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project.Domain.Entities;
using Project.Repository.Mappings.EntityFramework;
using System.IO;

namespace Project.Repository.Context
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlanetMap());

            modelBuilder.Entity<Planet>()
                .HasKey(p => p.Id);
        }
        public DbSet<Planet> Planets { get; set; }
    }
}
