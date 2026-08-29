using Carbon.Domain.Contracts.Data.Repositories;

namespace Carbon.Domain.Contracts.Data;

public interface IUnitOfWork
{ 
    Task SaveChangesAsync();
    IUserRepository UserRepository { get; }
    IRoleRepository RoleRepository { get; }
}