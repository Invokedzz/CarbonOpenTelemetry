using Carbon.Application.Dtos.Authentication;

namespace Carbon.Application.Contracts;

public interface IAuthUseCase
{
    Task<RegisterResponseDto> Register(RegisterRequestDto request);
    Task<LoginResponseDto> Login(LoginRequestDto request);
}