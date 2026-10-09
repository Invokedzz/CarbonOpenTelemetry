using Carbon.Domain.Contracts.Data;
using Carbon.Domain.Contracts.Data.Repositories;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IRoleRepository RoleRepository { get; }
    public IRefreshTokenRepository TokenRepository { get; }
    public IAirportRepository AirportRepository { get; }
    private readonly CarbonDbContext _context;

    public UnitOfWork(IUserRepository userRepository, IRoleRepository roleRepository,
        IRefreshTokenRepository tokenRepository,
        IAirportRepository airportRepository, CarbonDbContext context)
    {
        UserRepository = userRepository;
        RoleRepository = roleRepository;
        TokenRepository = tokenRepository;
        AirportRepository = airportRepository;
        _context = context;
    }
    
    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _context.SaveChangesAsync(ct);
    }
}