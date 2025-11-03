using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models.Entities;
using TaskManager.Repositories.Interfaces;

namespace TaskManager.Repositories.Implementations
{
    public class CategoryRepository : BaseRepository<Category>, IBaseRepository<Category>
    {
        public CategoryRepository(TaskManagerContext context) : base(context) { }
        public override async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbSet
                .Include(c => c.TaskItems)
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<Category?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(c => c.TaskItems)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
