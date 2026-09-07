using Carbon.Domain.Contracts.Data.Repositories;
using Carbon.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly CarbonDbContext _context;

    public RoleRepository(CarbonDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Role entity)
    {
        await _context.AddAsync(entity);
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await _context.Roles
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(e => e.Name == name);
    }

    public async Task<Role?> GetByIdAsync(Guid id)
    {
        return await _context.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}