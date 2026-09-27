using Carbon.Application.Contracts;
using Carbon.Application.Dtos.Emissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carbon.Core.Controllers;

[ApiController]
[Authorize]
[Route("Carbon/[controller]")]
public class EmissionsController : ControllerBase
{
    private readonly IEmissionsUseCase _useCase;

    public EmissionsController(IEmissionsUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpPost("Calculate")]
    public async Task<EmissionsResponseDto> Calculate(EmissionsRequestDto request, CancellationToken ct = default)
        => await _useCase.Calculate(request, ct);
}