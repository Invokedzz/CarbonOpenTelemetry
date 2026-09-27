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

    public async Task<CreateAccountResponse> CreateAccountAsync(User user, CancellationToken ct = default)
    {
        var search = await GetUserByEmailOrDefaultAsync(user.Email, ct);
        
        if (search is not null)
        {
            throw new BadRequestException($"{search.Email} is already registered!");
        }

        user.Password = GetHashedPassword(user, user.Password);
        user.Roles = [await GetRole(nameof(Roles.General), ct)];

        await _unitOfWork.UserRepository.AddAsync(user, ct);
        
        await _unitOfWork.SaveChangesAsync(ct);
        return new CreateAccountResponse(user, DateTime.Now, true);
    }

    public async Task<LoginResponse> LoginAsync(User user, CancellationToken ct = default)
    {
        var search = await GetUserByEmailOrDefaultAsync(user.Email, ct);

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

        var refreshToken = new RefreshToken
        {
            UserId = search.Id,
            Token = _securityPackManager.GenerateRefreshToken(),
            IsRevoked = false,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };

        await _unitOfWork.TokenRepository.AddAsync(refreshToken, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return new LoginResponse(
            accessToken,
            refreshToken.Token,
            DateTime.UtcNow.AddMinutes(8),
            refreshToken.IsRevoked);
    }

    public async Task<RefreshResponse> RefreshAsync(string token, CancellationToken ct = default)
    {
        var storedToken = await GetStoredToken(token, ct);
        var user = await GetUserByIdOrDefaultAsync(storedToken.UserId, ct);

        if (user is null)
        {
            throw new NotFoundException($"User with id: {storedToken.UserId} not found!");
        }
        
        var refreshToken = new RefreshToken
        {
            Token = _securityPackManager.GenerateRefreshToken(),
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false,
            UserId = user.Id
        };

        await _unitOfWork.TokenRepository.AddAsync(refreshToken, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return new RefreshResponse(
            _securityPackManager.GenerateToken(user),
            refreshToken.Token,
            DateTime.UtcNow.AddMinutes(8),
            refreshToken.IsRevoked);
    }

    private async Task<RefreshToken> GetStoredToken(string token, CancellationToken ct)
    {
        var storedToken = await _unitOfWork.TokenRepository
            .GetByTokenAsync(token, ct) ?? throw new BadRequestException($"Invalid token: {token}");

        storedToken.IsRevoked = true;
        
        return storedToken;
    }
    
    private async Task<User?> GetUserByEmailOrDefaultAsync(string email, CancellationToken ct)
        => await _unitOfWork.UserRepository.GetByEmailAsync(email, ct);

    private async Task<User?> GetUserByIdOrDefaultAsync(Guid id, CancellationToken ct)
        => await _unitOfWork.UserRepository.GetByIdAsync(id, ct);

    private async Task<Role> GetRole(string name, CancellationToken ct)
    {
        var matches = Enum.GetNames<Roles>()
            .Select(e => e)
            .FirstOrDefault(e => e == name);

        if (matches is null)
        {
            throw new BadRequestException("Assigned role does not exist!");
        }
        
        return await _unitOfWork.RoleRepository.GetByNameAsync(name, ct);
    }

    private string GetHashedPassword(User user, string password)
        => _securityPackManager.GetHashedPassword(user, password);
    
    private bool DoesPasswordMatch(User user, string currentPassword, string sentPassword) 
        => _securityPackManager.VerifyHashedPassword(user, currentPassword, sentPassword);
}