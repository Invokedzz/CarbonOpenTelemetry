using Carbon.Domain.Contracts.Data.Repositories;
using Carbon.Domain.Models;

namespace Infrastructure.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CarbonDbContext _context;

    public UserRepository(CarbonDbContext context)
    {
        _context = context;
    }
    
    public Task AddAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}