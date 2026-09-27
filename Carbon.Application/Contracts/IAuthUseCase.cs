using Carbon.Application.Dtos.Authentication;
using Carbon.Application.Dtos.Authentication.Login;
using Carbon.Application.Dtos.Authentication.Refresh;
using Carbon.Application.Dtos.Authentication.Register;

namespace Carbon.Application.Contracts;

public interface IAuthUseCase
{
    Task<RegisterResponseDto> Register(RegisterRequestDto request, CancellationToken ct);
    Task<LoginResponseDto> Login(LoginRequestDto request, CancellationToken ct);
    Task<RefreshResponseDto> Refresh(RefreshRequestDto request, CancellationToken ct);
}