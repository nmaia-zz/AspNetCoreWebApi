using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Domain.Contracts.Common
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity obj);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetByNameAsync(string name);
        Task<TEntity> GetByIdAsync(Guid? id);        
        Task RemoveAsync(TEntity obj);        
    }
}
