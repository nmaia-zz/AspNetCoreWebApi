using System;

namespace Project.WebApi.Models
{
    public class PlanetViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Climate { get; set; }
        public string Terrain { get; set; }
        public int TotalMovieApparitions { get; set; }
    }
}
