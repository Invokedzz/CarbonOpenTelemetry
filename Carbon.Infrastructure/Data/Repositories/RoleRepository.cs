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
    
    public async Task AddAsync(Role entity, CancellationToken ct = default)
    {
        await _context.AddAsync(entity, ct);
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await _context.Roles
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<Role> GetByNameAsync(string name, CancellationToken ct = default)
    {
        return await _context.Roles.FirstAsync(e => e.Name == name, ct);
    }

    public async Task<Role?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}