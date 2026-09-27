using Carbon.Domain.Contracts.Data.Repositories;

namespace Carbon.Domain.Contracts.Data;

public interface IUnitOfWork
{ 
    Task SaveChangesAsync(CancellationToken ct);
    IUserRepository UserRepository { get; }
    IRoleRepository RoleRepository { get; }
    IRefreshTokenRepository TokenRepository { get; }
}