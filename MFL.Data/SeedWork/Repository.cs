using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFL.Data.SeedWork
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly MFLContext _context;

        public Repository(MFLContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Get(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<EntityStatus> Put(int id, TEntity entity)
        {
            if (id != entity.Id)
            {
                return EntityStatus.UnmatchedId;
            }

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                {
                    return EntityStatus.EntityDoesntExist;
                }
                else
                {
                    throw;
                }
            }

            return EntityStatus.Updated;
        }

        public async Task<TEntity> Post(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<EntityStatus> Delete(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return EntityStatus.EntityDoesntExist;
            }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return EntityStatus.Deleted;
        }

        private bool EntityExists(int id)
        {
            return _context.Set<TEntity>().Any(e => e.Id == id);
        }
    }
}
