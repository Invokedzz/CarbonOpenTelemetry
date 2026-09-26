using Carbon.Domain.Models;

namespace Carbon.Domain.Contracts.Data.Repositories;

public interface IRefreshTokenRepository : ICarbonRepository<RefreshToken, Guid>
{
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct);
}