using Project.Domain.Entities;
using Project.Repository.Context;
using System.Linq;

namespace Project.Repository.Data
{
    public static class DbInitializer
    {
        public static void Initialize()
        {
            var context = new AppDbContext();
            context.Database.EnsureCreated();

            if (context.Planets.Any())
            {
                return; //data already exists in database!
            }

            var planets = new Planet[]
            {
                new Planet { Name = "Yavin IV", Climate = "Temperate, Tropical", Terrain = "Jungle, Rainforests" },
                new Planet { Name = "Dagobah", Climate = "Murky", Terrain = "Swamp, Jungles" },
                new Planet { Name = "Coruscant", Climate = "Temperate", Terrain = "Cityscape, Mountains" }
            };

            context.Planets.AddRange(planets);
            context.SaveChanges();
        }
    }
}
