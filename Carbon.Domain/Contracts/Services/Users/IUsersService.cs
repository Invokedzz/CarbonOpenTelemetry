namespace Carbon.Domain.Contracts.Services.Users;

public interface IUsersService
{
    Task<IReadOnlyList<UserSummary>> GetAllAsync(CancellationToken ct = default);
    Task<UserSummary> SetRoleAsync(Guid currentUserId, Guid userId, string role, CancellationToken ct = default);
    Task<bool> SetRoleByEmailAsync(string email, string role, CancellationToken ct = default);
}

public record UserSummary(Guid Id, string Username, string Email, string Role, bool HasFace, DateTime CreatedAt);
