namespace TaskManager.Services.Interfaces
{
    public interface IBaseService<TEntity, TRequest, TResponse>
        where TEntity : class
    {
        Task<IEnumerable<TResponse>> GetAllAsync();
        Task<TResponse?> GetAsync();
        Task<TResponse> CreateAsync(TEntity entity);
        Task UpdateAsync(int id, TRequest request);
        Task DeleteAsync(int id);

    }
}
