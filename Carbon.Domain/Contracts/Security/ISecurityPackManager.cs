using Carbon.Domain.Models;

namespace Carbon.Domain.Contracts.Security;

public interface ISecurityPackManager
{
    string GenerateToken(User user);
}