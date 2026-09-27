using Carbon.Application.Contracts;
using Carbon.Application.Dtos.Authentication.Login;
using Carbon.Application.Dtos.Authentication.Refresh;
using Carbon.Application.Dtos.Authentication.Register;
using Carbon.Domain.Contracts.Services.Authentication;
using Carbon.Domain.Models;

namespace Carbon.Application.UseCases;

public class AuthUseCase : IAuthUseCase
{
    private readonly IAuthService _authService;

    public AuthUseCase(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<RegisterResponseDto> Register(RegisterRequestDto request, CancellationToken ct = default)
    {
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password,
        };

        var createAcc = await _authService.CreateAccountAsync(user, ct);
        return new RegisterResponseDto(new UserDto(createAcc.User.Username, createAcc.User.Email, createAcc.IsActive, createAcc.CreatedAt));
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto request, CancellationToken ct = default)
    {
        var user = new User
        {
            Username = string.Empty,
            Email = request.Email,
            Password = request.Password
        };

        var authenticate = await _authService.LoginAsync(user, ct);
        
        return new LoginResponseDto(new TokenDto(
            authenticate.AccessToken,
            authenticate.RefreshToken,
            authenticate.ExpiresAt,
            authenticate.IsRevoked),
            DateTime.Now);
    }

    public async Task<RefreshResponseDto> Refresh(RefreshRequestDto request, CancellationToken ct = default)
    {
        var token = await _authService.RefreshAsync(request.Token, ct);
        
        return new RefreshResponseDto(new RefreshTokenDto(
            token.RefreshToken,
            token.AccessToken,
            token.ExpiresAt,
            token.IsRevoked));
    }
}