namespace Carbon.Domain.Contracts.Data.Repositories;

public interface ICarbonRepository<TEntity, TId>
{
    Task AddAsync(TEntity entity, CancellationToken ct);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct);
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken ct);
}