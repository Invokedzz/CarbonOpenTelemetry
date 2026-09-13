using Carbon.Domain.Models;

namespace Carbon.Domain.Contracts.Security;

public interface ISecurityPackManager
{
    string GenerateToken(User user);
    string GetHashedPassword(User user, string password);
    bool VerifyHashedPassword(User user, string hashedPassword, string password);
}