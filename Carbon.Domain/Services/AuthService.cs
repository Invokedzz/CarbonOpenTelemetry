using Carbon.Domain.Contracts.Data;
using Carbon.Domain.Contracts.Security;
using Carbon.Domain.Contracts.Services.Authentication;
using Carbon.Domain.Exceptions;
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
        var search = await GetUserByEmailOrDefault(user.Email);
        
        if (search is not null)
        {
            throw new BadRequestException($"{search.Email} is already registered!");
        }

        user.Password = GetHashedPassword(user, user.Password);
        user.Roles = [await GetRole(nameof(Roles.General))];

        await _unitOfWork.UserRepository.AddAsync(user);
        
        await _unitOfWork.SaveChangesAsync();
        return new CreateAccountResponse(user, DateTime.Now, true);
    }

    public async Task<LoginResponse> Login(User user)
    {
        var search = await GetUserByEmailOrDefault(user.Email);

        if (search is null)
        {
            throw new NotFoundException($"User with email: {user.Email} not found!");
        }
        
        var matches = DoesPasswordMatch(search, search.Password, user.Password);
        
        if (!matches)
        {
            throw new BadRequestException("Passwords do not match. Please, try again!");
        }
        
        var accessToken = _securityPackManager.GenerateToken(search);
        return new LoginResponse(accessToken, DateTime.Now.AddMinutes(8), false);
    }

    private async Task<User?> GetUserByEmailOrDefault(string email)
        => await _unitOfWork.UserRepository.GetByEmailAsync(email);

    private async Task<Role> GetRole(string name)
    {
        var matches = Enum.GetNames<Roles>()
            .Select(e => e)
            .FirstOrDefault(e => e == name);

        if (matches is null)
        {
            throw new BadRequestException("Assigned role does not exist!");
        }
        
        return await _unitOfWork.RoleRepository.GetByNameAsync(name);
    }

    private string GetHashedPassword(User user, string password)
        => _securityPackManager.GetHashedPassword(user, password);
    
    private bool DoesPasswordMatch(User user, string currentPassword, string sentPassword) 
        => _securityPackManager.VerifyHashedPassword(user, currentPassword, sentPassword);
}