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
    /// <summary>
    /// Planet Controller, responsible to deliver the methods to handle planets.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetsController : ControllerBase
    {
        private readonly IPlanetRepository _planetRepository;
        private readonly ISwApiConnector _swApiConnector;
        private readonly IMapper _mapper;

        /// <summary>
        /// Controller Constructor.
        /// </summary>
        /// <param name="planetRepository">IPlanetRepository planetRepository</param>
        /// <param name="swApiConnector">ISwApiConnector swApiConnector</param>
        /// <param name="mapper">IMapper mapper</param>
        public PlanetsController(IPlanetRepository planetRepository, ISwApiConnector swApiConnector, IMapper mapper)
        {
            _planetRepository = planetRepository;
            _swApiConnector = swApiConnector;
            _mapper = mapper;
        }

        /// <summary>
        /// Method to return all the registred planets.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     
        ///         GET /getAll
        ///         {}
        ///         
        /// </remarks>
        /// <returns>IEnumerable of PlanetForGetViewModel</returns>
        /// <response code="200">Returns all the Planets</response>
        /// <response code="400">If the item is null</response>
        /// <response code="500">Some internal error has occured in the server</response>
        [HttpGet]
        [Route("getAll")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
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

        /// <summary>
        /// This method is responsible to get the number of planet apparitions on the movies.
        /// </summary>
        /// <param name="planet">PlanetForGetViewModel planet</param>
        private void SetTotalMovieApparitionsOfThePlanet(PlanetForGetViewModel planet)
        {
            var swApiResponseData = (RootObjectViewModel)JsonConvert.DeserializeObject<RootObjectViewModel>(_swApiConnector.GetAllMovieApparitionsByPlanet(planet.Name));

            planet.TotalMovieApparitions = (swApiResponseData.results.Count > 0)
                ? swApiResponseData.results[0].films.Count
                : 0;
        }

        /// <summary>
        /// Method used to search a planet by Id.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     
        ///         GET /getById/{id}
        ///         {        
        ///             'id': '70aca700-fbdd-4d4d-0900-08d66b7edd26'
        ///         }
        ///         
        /// </remarks>
        /// <param name="id">Guid id</param>
        /// <returns>PlanetForGetViewModel</returns>
        /// <response code="200">Returns a single Planet</response>
        /// <response code="400">If the item is null</response>
        /// <response code="500">Some internal error has occured in the server</response>
        [HttpGet]
        [Route("getById/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
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

        /// <summary>
        /// Method used to search a Planet by name.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     
        ///         GET /getByName/{planetName}
        ///         {
        ///             'planetName': 'Some name'
        ///         }
        /// 
        /// </remarks>
        /// <param name="planetName">string planetName</param>
        /// <returns>PlanetForGetViewModel</returns>
        /// <response code="200">Returns a single Planet</response>
        /// <response code="400">If the item is null</response>
        /// <response code="500">Some internal error has occured in the server</response>
        [HttpGet]
        [Route("getByName/{planetName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
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

        /// <summary>
        /// Method used to regiser a new planet into database.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     
        ///         POST /create
        ///         {
        ///             'name': 'Some name',
        ///             'climate': 'One or many climates',
        ///             'terrain': 'One or many terrains'
        ///         }
        ///         
        /// </remarks>
        /// <param name="model">PlanetForPostViewModel model</param>
        /// <returns>HttpStatusCode</returns>
        /// <response code="200">It means the Planet was registred successfully</response>
        /// <response code="400">If the item is null</response>
        /// <response code="500">Some internal error has occured in the server</response>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
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

        /// <summary>
        /// Method used to delete a planet from database.
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     
        ///         DELETE /remove/{id}
        ///         {
        ///             'id': '70aca700-fbdd-4d4d-0900-08d66b7edd26' 
        ///         }
        ///         
        /// </remarks>
        /// <param name="id">Guid id</param>
        /// <returns>HttpStatusCode</returns>
        /// <response code="200">It means the Planet was removed successfully</response>
        /// <response code="400">If the item is null</response>
        /// <response code="500">Some internal error has occured in the server</response>
        [HttpDelete]
        [Route("remove/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
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
