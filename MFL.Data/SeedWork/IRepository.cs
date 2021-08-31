using System.Collections.Generic;
using System.Threading.Tasks;

namespace MFL.Data.SeedWork
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(int id);
        Task<EntityStatus> Put(int id, TEntity entity);
        Task<TEntity> Post(TEntity entity);
        Task<EntityStatus> Delete(int id);
    }
}
