using AutoMapper;
using Project.Domain.Entities;
using Project.WebApi.Models;

namespace Project.WebApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PlanetViewModel, Planet>();
            CreateMap<Planet, PlanetViewModel>();
        }
    }
}
