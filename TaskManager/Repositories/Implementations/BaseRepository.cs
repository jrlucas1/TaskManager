using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Repositories.Interfaces;

namespace TaskManager.Repositories.Implementations
{
    public abstract class BaseRepository <TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly TaskManagerContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(TaskManagerContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity != null) {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task<bool> ExistsAsync (int id)
        {
            return await _dbSet.FindAsync(id) != null;
        }

    }
}
