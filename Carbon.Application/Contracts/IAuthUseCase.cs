using Carbon.Application.Dtos.Authentication;
using Carbon.Application.Dtos.Authentication.Login;
using Carbon.Application.Dtos.Authentication.Register;

namespace Carbon.Application.Contracts;

public interface IAuthUseCase
{
    Task<RegisterResponseDto> Register(RegisterRequestDto request);
    Task<LoginResponseDto> Login(LoginRequestDto request);
}