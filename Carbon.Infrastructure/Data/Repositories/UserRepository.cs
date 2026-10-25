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

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Users
            .AsSplitQuery()
            .ToListAsync(ct);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(e => e.Roles)
            .FirstOrDefaultAsync(e => e.Id == id, ct);
    }

     public Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        return _context.Users
            .Include(e => e.Roles)
            .FirstOrDefaultAsync(e => e.Email == email, ct);
    }

    public async Task<IEnumerable<User>> GetAllWithRolesAsync(CancellationToken ct = default)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(e => e.Roles)
            .OrderBy(e => e.CreatedAt)
            .ToListAsync(ct);
    }

    // Sem AsNoTracking: usado quando o usuário vai ser alterado (ex.: troca de perfil)
    public async Task<User?> GetByIdForUpdateAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Users
            .Include(e => e.Roles)
            .FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    public async Task UpdateFaceEmbeddingAsync(Guid userId, byte[] faceEmbedding, CancellationToken ct = default)
    {
        await _context.Users
            .Where(e => e.Id == userId)
            .ExecuteUpdateAsync(s => s.SetProperty(e => e.FaceEmbedding, faceEmbedding), ct);
    }
}