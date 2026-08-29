using Carbon.Domain.Contracts.Data;
using Carbon.Domain.Contracts.Data.Repositories;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IRoleRepository RoleRepository { get; }
    private readonly CarbonDbContext _context;

    public UnitOfWork(IUserRepository userRepository, IRoleRepository roleRepository, CarbonDbContext context)
    {
        UserRepository = userRepository;
        RoleRepository = roleRepository;
        _context = context;
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}