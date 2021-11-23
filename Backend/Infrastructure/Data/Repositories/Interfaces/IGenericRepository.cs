using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Data.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(string[] includes);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(string[] includes);
        TEntity GetById(object id);
        Task<TEntity> GetByIdAsync(object id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);

        Task<int> SaveAsync();
    }
}
