using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Contracts.Repositories;
using Project.Domain.Entities;
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
        private readonly IMapper _mapper;

        public PlanetsController(IPlanetRepository planetRepository, IMapper mapper)
        {
            _planetRepository = planetRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult<IEnumerable<PlanetViewModel>>> GetAllPlanets()
        {
            try
            {
                var planets = await _planetRepository.GetAllAsync();

                if (planets == null)
                    return NotFound();

                var response = _mapper.Map<List<PlanetViewModel>>(planets);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
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

                var response = _mapper.Map<PlanetViewModel>(planet);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet]
        [Route("getByName/{name}")]
        public IActionResult GetPlanetByName([FromRoute] string name)
        {
            try
            {
                if (name == null)
                    return BadRequest();

                var planet = _planetRepository.GetByNameAsync(name);

                if (planet == null)
                    return NotFound();

                var response = _mapper.Map<PlanetViewModel>(planet);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateNewPlanet([FromBody] PlanetViewModel model)
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
