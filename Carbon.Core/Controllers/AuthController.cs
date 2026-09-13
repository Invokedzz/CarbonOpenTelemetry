using Carbon.Application.Contracts;
using Carbon.Application.Dtos.Authentication;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<RegisterResponseDto> Register(RegisterRequestDto request)
        => await _useCase.Register(request);

    [HttpPost("/Login")]
    public async Task<LoginResponseDto> Login(LoginRequestDto request)
        => await _useCase.Login(request);
}