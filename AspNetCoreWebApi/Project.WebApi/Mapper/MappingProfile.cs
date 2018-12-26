using AutoMapper;
using Project.Domain.Entities;
using Project.WebApi.Models;

namespace Project.WebApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PlanetForGetViewModel, Planet>();
            CreateMap<Planet, PlanetForGetViewModel>();

            CreateMap<PlanetForPostViewModel, Planet>();
        }
    }
}
