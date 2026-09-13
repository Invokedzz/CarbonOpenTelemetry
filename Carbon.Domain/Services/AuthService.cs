using Carbon.Domain.Contracts.Data;
using Carbon.Domain.Contracts.Security;
using Carbon.Domain.Contracts.Services.Authentication;
using Carbon.Domain.Models;

namespace Carbon.Domain.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISecurityPackManager _securityPackManager;

    public AuthService(IUnitOfWork unitOfWork, ISecurityPackManager securityPackManager)
    {
        _unitOfWork = unitOfWork;
        _securityPackManager = securityPackManager;
    }

    public async Task<CreateAccountResponse> CreateAccount(User user)
    {
        await Task.Delay(1000);
        throw new NotImplementedException();
    }

    public async Task<LoginResponse> Login(User user)
    {
        var search = await GetUserOrDefault(user.Email);
        var matches = DoesPasswordMatch(search, search.Password, user.Password);
        
        if (!matches)
        {
            throw new ArgumentException("Passwords do not match. Please, try again!");
        }
        
        var accessToken = _securityPackManager.GenerateToken(search);
        return new LoginResponse(accessToken, DateTime.Now.AddMinutes(8), false);
    }

    private async Task<User> GetUserOrDefault(string email)
    {
        var search = await _unitOfWork.UserRepository.GetByEmailAsync(email);
        return search ?? throw new ArgumentException($"User with email: {email} not found!");
    }
    
    private bool DoesPasswordMatch(User user, string currentPassword, string sentPassword) 
        => _securityPackManager.VerifyHashedPassword(user, currentPassword, sentPassword);
}