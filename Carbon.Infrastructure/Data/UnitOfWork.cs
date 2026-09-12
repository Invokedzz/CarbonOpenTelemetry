using Carbon.Domain.Contracts.Data;
using Carbon.Domain.Contracts.Data.Repositories;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IRoleRepository RoleRepository { get; }
    public IRefreshTokenRepository TokenRepository { get; }
    private readonly CarbonDbContext _context;

    public UnitOfWork(IUserRepository userRepository, IRoleRepository roleRepository,
        IRefreshTokenRepository tokenRepository, CarbonDbContext context)
    {
        UserRepository = userRepository;
        RoleRepository = roleRepository;
        TokenRepository = tokenRepository;
        _context = context;
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}