using Carbon.Application.Contracts;
using Carbon.Application.Dtos.Authentication.Login;
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

    public async Task<RegisterResponseDto> Register(RegisterRequestDto request)
    {
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password,
        };

        var createAcc = await _authService.CreateAccount(user);
        return new RegisterResponseDto(new UserDto(createAcc.User.Username, createAcc.User.Email, createAcc.IsActive, createAcc.CreatedAt));
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto request)
    {
        var user = new User
        {
            Username = string.Empty,
            Email = request.Email,
            Password = request.Password
        };

        var authenticate = await _authService.Login(user);
        return new LoginResponseDto(new TokenDto(authenticate.AccessToken, authenticate.ExpiresAt, authenticate.IsRevoked), DateTime.Now);
    }
}