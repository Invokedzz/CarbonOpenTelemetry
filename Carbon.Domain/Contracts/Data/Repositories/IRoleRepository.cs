using Carbon.Domain.Models;

namespace Carbon.Domain.Contracts.Data.Repositories;

public interface IRoleRepository : ICarbonRepository<Role, Guid>
{
    Task<Role?> GetByNameAsync(string name);
}