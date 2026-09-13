using Carbon.Domain.Models;

namespace Carbon.Domain.Contracts.Services.Authentication;

public interface IAuthService
{
    Task<CreateAccountResponse> CreateAccount(User user);
    Task<LoginResponse> Login(User user);
}