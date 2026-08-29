using Carbon.Domain.Models;

namespace Carbon.Domain.Contracts.Data.Repositories;

public interface IUserRepository : ICarbonRepository<User, Guid>
{
    
}