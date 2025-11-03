namespace TaskManager.Repositories.Interfaces
{
    public interface IBaseRepository <TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task ExistsAsync(int id);

    }
}
