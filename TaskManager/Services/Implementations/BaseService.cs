using TaskManager.Repositories.Interfaces;
using TaskManager.Services.Interfaces;

namespace TaskManager.Services.Implementations
{
    public abstract class BaseService<TEntity, TRequest, TResponse> : IBaseService<TEntity, TRequest, TResponse>
        where TEntity : class
    {
        protected readonly IBaseRepository<TEntity> _repository;

        protected BaseService(IBaseRepository<TEntity> repository) 
        {
            _repository = repository;
        }

        protected abstract TEntity MapToEntity (TRequest request);
        protected abstract TResponse MapToResponse (TEntity entity);
        protected abstract void UpdateEntity (TEntity entity, TRequest);

        public virtual async Task<IEnumerable<TResponse>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(MapToResponse);
        }

        public virtual async Task<TResponse?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            return entity != null ? MapToResponse(entity) : default;
        }

        public virtual async Task<TResponse> CreateAsync(TRequest request)
        {
            var entity = MapToEntity (request);
            var createdEntity = await _repository.AddAsync(entity);
            return MapToResponse(createdEntity);
        }

        public virtual async Task UpdateAsync(int id, TRequest request)
        {
            var entity = await _repository.GetByIdAsync (id);
            if (entity == null)
                throw new KeyNotFoundException($"Entity with id {id} not found");
             
            UpdateEntity(entity, request);
            await _repository.UpdateAsync(entity);

        }

        public virtual async Task DeleteAsync(int id)
        {
            var exists = await _repository.ExistsAsync(id);

            if (!exists)
                throw new KeyNotFoundException($"Entity with id {id} not found");
            
            await _repository.DeleteAsync(id);
        }
    }
}
