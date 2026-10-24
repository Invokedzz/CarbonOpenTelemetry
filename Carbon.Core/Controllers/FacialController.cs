using Carbon.Application.Contracts;
using Carbon.Application.Dtos.Facial;
using Carbon.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carbon.Core.Controllers;

[ApiController]
[Authorize]
[Route("Carbon/[controller]")]
public class FacialController : ControllerBase
{
    private readonly IFacialUseCase _useCase;

    public FacialController(IFacialUseCase useCase)
    {
        _useCase = useCase;
    }

    // Cadastra (ou substitui) o rosto do usuário logado
    [HttpPost("Register")]
    public async Task<FaceRegisterResponseDto> Register(FaceImageRequestDto request, CancellationToken ct = default)
        => await _useCase.Register(GetUserId(), request, ct);

    // Compara a foto com o rosto cadastrado do usuário logado
    [HttpPost("Verify")]
    public async Task<FaceVerificationResponseDto> Verify(FaceImageRequestDto request, CancellationToken ct = default)
        => await _useCase.Verify(GetUserId(), request, ct);

    // O id do usuário vem do token JWT (claim "sub")
    private Guid GetUserId()
    {
        var sub = User.FindFirst("sub")?.Value;

        return Guid.TryParse(sub, out var userId)
            ? userId
            : throw new UnauthorizedException("Invalid token: user id not found.");
    }
}