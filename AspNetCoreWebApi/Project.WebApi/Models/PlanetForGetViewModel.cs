using System;

namespace Project.WebApi.Models
{
    /// <summary>
    /// A Planet view model to be used only in requests of GET type.
    /// </summary>
    public class PlanetForGetViewModel
    {
        /// <summary>
        /// Planet Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Planet Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Planet Climate
        /// </summary>
        public string Climate { get; set; }

        /// <summary>
        /// Planet Terrain
        /// </summary>
        public string Terrain { get; set; }

        /// <summary>
        /// The total times a Planet had appeared on the movies.
        /// </summary>
        public int TotalMovieApparitions { get; set; }
    }
}
