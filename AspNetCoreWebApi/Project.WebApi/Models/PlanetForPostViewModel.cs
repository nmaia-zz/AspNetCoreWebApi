using System.ComponentModel.DataAnnotations;

namespace Project.WebApi.Models
{
    /// <summary>
    /// A Planet view model to be used only in requests of POST type.
    /// </summary>
    public class PlanetForPostViewModel
    {
        /// <summary>
        /// Planet Name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Planet Climate
        /// </summary>
        [Required]
        public string Climate { get; set; }

        /// <summary>
        /// Planet Terrain
        /// </summary>
        [Required]
        public string Terrain { get; set; }
    }
}
