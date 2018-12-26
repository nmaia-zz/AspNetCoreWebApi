using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Domain.Contracts;
using Project.Domain.Entities;
using Project.Repository.Repositories.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Project.Repository.Repositories
{
    public class PlanetRepository : RepositoryBase<Planet>, IPlanetRepository
    {
        public override async Task<IEnumerable<Planet>> GetByNameAsync(string name)
            => await db.Set<Planet>().Where(p => p.Name.ToLower().Contains(name.ToLower())).ToListAsync();
    }
}
