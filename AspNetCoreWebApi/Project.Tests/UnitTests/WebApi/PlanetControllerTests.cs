using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Project.Domain.Contracts.Repositories;
using Project.Domain.Entities;
using Project.RestfulConnector.Contracts;
using Project.WebApi.Controllers;
using Project.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Project.Tests.UnitTests.WebApi
{
    public class PlanetControllerTests
    {
        [Fact]
        public async Task GetAllPlanetsCheckResponseTypeTest()
        {
            //Arrange
            var mockPlanetRepository = new Mock<IPlanetRepository>();
            mockPlanetRepository.Setup(s => s.GetAllAsync())
                .Returns(GetPlanetsForTest());

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(s => s.Map<Task<IEnumerable<PlanetForGetViewModel>>>(GetPlanetsForGetTest()))
                .Returns(GetPlanetsForGetTest());

            var mockSwApiConnector = new Mock<ISwApiConnector>();
            mockSwApiConnector.Setup(s => s.GetAllMovieApparitionsByPlanet("Planet 1"))
                .Returns(1);

            var controller = new PlanetsController(mockPlanetRepository.Object, mockSwApiConnector.Object, mockMapper.Object);

            //Act
            var response = await controller.GetAllPlanets();

            //Assert
            Assert.IsType<ObjectResult>(response.Result);
        }

        [Fact]
        public async Task GetPlanetByIdCheckResponseTypeTest()
        {
            //Arrange
            var mockPlanetRepository = new Mock<IPlanetRepository>();
            mockPlanetRepository.Setup(s => s.GetByIdAsync(Guid.Parse("37bb3789-894a-432a-b9c2-8c9602dfa0c7")))
                .Returns(GetPlanetForTest());

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(s => s.Map<Task<Planet>>(GetPlanetForTest()))
                .Returns(GetPlanetForTest());

            var mockSwApiConnector = new Mock<ISwApiConnector>();
            mockSwApiConnector.Setup(s => s.GetAllMovieApparitionsByPlanet("Planet 4"))
                .Returns(1);

            var controller = new PlanetsController(mockPlanetRepository.Object, mockSwApiConnector.Object, mockMapper.Object);

            //Act
            var response = await controller.GetPlanetById(Guid.Parse("37bb3789-894a-432a-b9c2-8c9602dfa0c7"));

            //Assert
            Assert.IsType<ObjectResult>(response);
        }

        [Fact]
        public void GetPlanetByNameCheckResponseTypeTest()
        {
            //Arrange
            var mockPlanetRepository = new Mock<IPlanetRepository>();
            mockPlanetRepository.Setup(s => s.GetByNameAsync("Planet 4"))
                .Returns(GetPlanetNotTaskForTest());

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(s => s.Map<Task<Planet>>(GetPlanetForTest()))
                .Returns(GetPlanetForTest());

            var mockSwApiConnector = new Mock<ISwApiConnector>();
            mockSwApiConnector.Setup(s => s.GetAllMovieApparitionsByPlanet("Planet 4"))
                .Returns(1);

            var controller = new PlanetsController(mockPlanetRepository.Object, mockSwApiConnector.Object, mockMapper.Object);

            //Act
            var response = controller.GetPlanetByName("Planet 4");

            //Assert
            Assert.IsType<ObjectResult>(response);
        }

        private async Task<IEnumerable<PlanetForGetViewModel>> GetPlanetsForGetTest()
        {
            return new List<PlanetForGetViewModel>()
            {
                new PlanetForGetViewModel
                {
                    Id = Guid.Parse("048c24f2-0634-4d63-ad31-19a40cc9c1b5"), Climate = "Climate 1, Climate 2", Name = "Planet 1", Terrain = "Terrain 1, Terrain 2", TotalMovieApparitions = 1
                },
                new PlanetForGetViewModel
                {
                    Id = Guid.Parse("7f40c15c-ab23-4d9f-ab35-df1353a46236"), Climate = "Climate 1, Climate 2", Name = "Planet 2", Terrain = "Terrain 1, Terrain 2", TotalMovieApparitions = 3
                },
                new PlanetForGetViewModel
                {
                    Id = Guid.Parse("ec4edd9c-aeac-41a3-87e6-c8c4e68290d9"), Climate = "Climate 1, Climate 2", Name = "Planet 3", Terrain = "Terrain 1, Terrain 2", TotalMovieApparitions = 4
                }
            };
        }

        private IEnumerable<PlanetForPostViewModel> GetPlanetsForPostTest()
        {
            return new List<PlanetForPostViewModel>()
            {
                new PlanetForPostViewModel
                {
                    Climate = "Climate 1, Climate 2", Name = "Planet 4", Terrain = "Terrain 1, Terrain 2"
                },
                new PlanetForPostViewModel
                {
                    Climate = "Climate 1, Climate 2", Name = "Planet 5", Terrain = "Terrain 1, Terrain 2"
                },
                new PlanetForPostViewModel
                {
                    Climate = "Climate 1, Climate 2", Name = "Planet 6", Terrain = "Terrain 1, Terrain 2"
                }
            };
        }

        private async Task<IEnumerable<Planet>> GetPlanetsForTest()
        {
            return new List<Planet>()
            {
                new Planet
                {
                    Id = Guid.Parse("37bb3789-894a-432a-b9c2-8c9602dfa0c7"), Climate = "Climate 1, Climate 2", Name = "Planet 4", Terrain = "Terrain 1, Terrain 2"
                },
                new Planet
                {
                    Id = Guid.Parse("6ab85753-c4cb-4fd1-8a83-e23789aed430"), Climate = "Climate 1, Climate 2", Name = "Planet 5", Terrain = "Terrain 1, Terrain 2"
                },
                new Planet
                {
                    Id = Guid.Parse("a0a44e08-8445-4c45-b4ac-b3ae3d7faec2"), Climate = "Climate 1, Climate 2", Name = "Planet 6", Terrain = "Terrain 1, Terrain 2"
                }
            };
        }

        private async Task<Planet> GetPlanetForTest()
            => new Planet
            {
                Id = Guid.Parse("37bb3789-894a-432a-b9c2-8c9602dfa0c7"),
                Climate = "Climate 1, Climate 2",
                Name = "Planet 4",
                Terrain = "Terrain 1, Terrain 2"
            };

        private Planet GetPlanetNotTaskForTest()
            => new Planet
            {
                Id = Guid.Parse("37bb3789-894a-432a-b9c2-8c9602dfa0c7"),
                Climate = "Climate 1, Climate 2",
                Name = "Planet 4",
                Terrain = "Terrain 1, Terrain 2"
            };
    }
}
