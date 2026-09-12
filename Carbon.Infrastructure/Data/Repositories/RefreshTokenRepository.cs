using Carbon.Domain.Contracts.Data.Repositories;
using Carbon.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly CarbonDbContext _context;

    public RefreshTokenRepository(CarbonDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(RefreshToken entity)
    {
        await _context.Tokens.AddAsync(entity);
    }

    public async Task<IEnumerable<RefreshToken>> GetAllAsync()
    {
        return await _context.Tokens
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<RefreshToken?> GetByIdAsync(Guid id)
    {
        return await _context.Tokens
            .Where(e => !e.IsRevoked)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}   