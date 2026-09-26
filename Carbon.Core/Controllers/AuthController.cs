using Carbon.Application.Contracts;
using Carbon.Application.Dtos.Authentication;
using Carbon.Application.Dtos.Authentication.Login;
using Carbon.Application.Dtos.Authentication.Register;
using Carbon.Core.Contracts;
using Carbon.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Carbon.Core.Controllers;

[ApiController]
[Route("Carbon/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthUseCase _useCase;

    public AuthController(IAuthUseCase authUseCase)
    {
        _useCase = authUseCase;
    }

    [HttpPost("/Register")]
    [EnableRateLimiting(nameof(RateLimiterPolicies.SessionPolicy))]
    public async Task<RegisterResponseDto> Register(RegisterRequestDto request)
        => await _useCase.Register(request);

    [HttpPost("/Login")]
    [EnableRateLimiting(nameof(RateLimiterPolicies.SessionPolicy))]
    public async Task<LoginResponseDto> Login(LoginRequestDto request)
        => await _useCase.Login(request);
}