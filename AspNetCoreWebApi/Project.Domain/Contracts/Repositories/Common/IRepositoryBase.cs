using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Domain.Contracts.Repositories.Common
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity obj);
        Task<IEnumerable<TEntity>> GetAllAsync();
        TEntity GetByNameAsync(string planetName);
        Task<TEntity> GetByIdAsync(Guid? id);        
        Task RemoveAsync(TEntity obj);        
    }
}
