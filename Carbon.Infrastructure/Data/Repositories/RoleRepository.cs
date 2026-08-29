using Carbon.Domain.Contracts.Data.Repositories;
using Carbon.Domain.Models;

namespace Infrastructure.Data.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly CarbonDbContext _context;

    public RoleRepository(CarbonDbContext context)
    {
        _context = context;
    }
    
    public Task AddAsync(Role entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Role>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Role> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}