using Carbon.Domain.Models;

namespace Carbon.Domain.Contracts.Services.Authentication
{
    public record CreateAccountResponse(User User, DateTime CreatedAt, bool IsActive);
    public record LoginResponse(string AccessToken, DateTime ExpiresAt, bool IsRevoked);
    public enum Roles
    {
        General,
        Director,
        Admin
    }
}