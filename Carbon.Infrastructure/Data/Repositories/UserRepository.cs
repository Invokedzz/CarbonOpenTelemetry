using Carbon.Domain.Contracts.Data.Repositories;
using Carbon.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CarbonDbContext _context;

    public UserRepository(CarbonDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(User entity, CancellationToken ct = default)
    {
        await _context.Users.AddAsync(entity, ct);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(e => e.Roles)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        return _context.Users
            .Include(e => e.Roles)
            .FirstOrDefaultAsync(e => e.Email == email, ct);
    }
}