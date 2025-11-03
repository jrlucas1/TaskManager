using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models.Entities;
using TaskManager.Repositories.Interfaces;

namespace TaskManager.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IBaseRepository<User>
    {
        public UserRepository(TaskManagerContext context) : base(context) { }
        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbSet
                .Include(u => u.TaskItems)
                .AsNoTracking()
                .ToListAsync();
        }
        public override async Task<User?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(u => u.TaskItems)
                .FirstOrDefaultAsync(u => u.ID == id);
        }
    }
}
