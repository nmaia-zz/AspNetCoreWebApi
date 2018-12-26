using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.WebApi.Models;

namespace Project.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetsController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllPlanets()
        {
            throw new NotImplementedException();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Route("getById")]
        public async Task<ActionResult<string>> GetPlanetById([FromBody] Guid id)
        {
            throw new NotImplementedException();
        }

        // GET api/values/5
        [HttpGet("{name}")]
        [Route("getByName")]
        public async Task<ActionResult<string>> GetPlanetByName([FromBody] string name)
        {
            throw new NotImplementedException();
        }

        // POST api/values
        [HttpPost]
        [Route("create")]
        public async Task CreateNewPlanet([FromBody] PlanetViewModel model)
        {
            throw new NotImplementedException();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Route("remove")]
        public async Task Delete([FromBody] Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
