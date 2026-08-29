namespace Carbon.Domain.Contracts.Data.Repositories;

public interface ICarbonRepository<TEntity, TId>
{
    Task AddAsync(TEntity entity);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(TId id);
}