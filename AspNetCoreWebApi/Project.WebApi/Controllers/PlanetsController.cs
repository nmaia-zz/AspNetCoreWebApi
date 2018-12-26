using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project.Domain.Contracts.Repositories;
using Project.Domain.Entities;
using Project.RestfulConnector.Contracts;
using Project.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetsController : ControllerBase
    {
        private readonly IPlanetRepository _planetRepository;
        private readonly ISwApiConnector _swApiConnector;
        private readonly IMapper _mapper;

        public PlanetsController(IPlanetRepository planetRepository, ISwApiConnector swApiConnector, IMapper mapper)
        {
            _planetRepository = planetRepository;
            _swApiConnector = swApiConnector;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult<IEnumerable<PlanetForGetViewModel>>> GetAllPlanets()
        {
            try
            {
                var planets = await _planetRepository.GetAllAsync();

                if (planets == null)
                    return NotFound();

                var response = _mapper.Map<List<PlanetForGetViewModel>>(planets);

                foreach (var planet in response)
                    SetTotalMovieApparitionsOfThePlanet(planet);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        private void SetTotalMovieApparitionsOfThePlanet(PlanetForGetViewModel planet)
        {
            var swApiResponseData = (RootObject)JsonConvert.DeserializeObject<RootObject>(_swApiConnector.GetAllMovieApparitionsByPlanet(planet.Name));

            planet.TotalMovieApparitions = (swApiResponseData.results.Count > 0)
                ? swApiResponseData.results[0].films.Count
                : 0;
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetPlanetById([FromRoute] Guid id)
        {
            try
            {
                if (id == null)
                    return BadRequest();

                var planet = await _planetRepository.GetByIdAsync(id);

                if (planet == null)
                    return NotFound();

                var response = _mapper.Map<PlanetForGetViewModel>(planet);

                SetTotalMovieApparitionsOfThePlanet(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet]
        [Route("getByName/{planetName}")]
        public IActionResult GetPlanetByName([FromRoute] string planetName)
        {
            try
            {
                if (planetName == null)
                    return BadRequest();

                var planet = _planetRepository.GetByNameAsync(planetName);

                if (planet == null)
                    return NotFound();

                var response = _mapper.Map<PlanetForGetViewModel>(planet);

                SetTotalMovieApparitionsOfThePlanet(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateNewPlanet([FromBody] PlanetForPostViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (model == null)
                    return BadRequest();

                var planet = _mapper.Map<Planet>(model);

                await _planetRepository.AddAsync(planet);

                return Ok();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("This planet already exists in database. Try another one.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete]
        [Route("remove/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                if (id == null)
                    return BadRequest();

                var planet = await _planetRepository.GetByIdAsync(id);

                if (planet == null)
                    return NotFound();

                await _planetRepository.RemoveAsync(planet);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
