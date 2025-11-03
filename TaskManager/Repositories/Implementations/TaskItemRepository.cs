using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models.Entities;
using TaskManager.Repositories.Interfaces;

namespace TaskManager.Repositories.Implementations
{
    public class TaskItemRepository : BaseRepository<TaskItem>, IBaseRepository<TaskItem>
    {
        public TaskItemRepository (TaskManagerContext context) : base (context) { }
        public override async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _dbSet
                  .Include(t => t.User)
                  .Include(t => t.Category)
                  .AsNoTracking()
                  .ToListAsync();
        }

        public override async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(t => t.User)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t=> t.Id == id);
        }

        public
    }
}
