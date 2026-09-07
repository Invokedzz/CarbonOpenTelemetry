using Carbon.Domain.Models;

namespace Carbon.Domain.Contracts.Services;

public interface IUserService
{
    Task CreateAsync(User user);
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
}