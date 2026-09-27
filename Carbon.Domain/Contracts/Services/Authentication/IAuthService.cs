using Carbon.Domain.Models;

namespace Carbon.Domain.Contracts.Services.Authentication;

public interface IAuthService
{
    Task<CreateAccountResponse> CreateAccountAsync(User user, CancellationToken ct);
    Task<LoginResponse> LoginAsync(User user, CancellationToken ct);
    Task<RefreshResponse> RefreshAsync(string token, CancellationToken ct);
}