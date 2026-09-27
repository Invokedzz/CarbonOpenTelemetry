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
    
    public async Task AddAsync(RefreshToken entity, CancellationToken ct = default)
    {
        await _context.Tokens.AddAsync(entity, ct);
    }

    public async Task<IEnumerable<RefreshToken>> GetAllAsync()
    {
        return await _context.Tokens
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<RefreshToken?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Tokens
            .FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct = default)
    {
        return await _context.Tokens
            .Where(e => !e.IsRevoked)
            .FirstOrDefaultAsync(e => e.Token == token, ct);
    }
}   