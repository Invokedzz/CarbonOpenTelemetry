using Carbon.Domain.Models;

namespace Carbon.Domain.Contracts.Data.Repositories;

public interface IUserRepository : ICarbonRepository<User, Guid>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken ct);
    Task<IEnumerable<User>> GetAllWithRolesAsync(CancellationToken ct);
    Task<User?> GetByIdForUpdateAsync(Guid id, CancellationToken ct);
    Task UpdateFaceEmbeddingAsync(Guid userId, byte[] faceEmbedding, CancellationToken ct);
}