using AutoMapper;
using Project.Domain.Entities;
using Project.WebApi.Models;

namespace Project.WebApi.Mapper
{
    /// <summary>
    /// Automapper profile class
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Automapper profile constructor
        /// </summary>
        public MappingProfile()
        {
            CreateMap<PlanetForGetViewModel, Planet>();
            CreateMap<Planet, PlanetForGetViewModel>();

            CreateMap<PlanetForPostViewModel, Planet>();
        }
    }
}
