using Carbon.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Carbon.Core.Controllers;

[ApiController]
[Route("Carbon/[controller]")]
public class EmissionsController : ControllerBase
{
    private readonly IEmissionsUseCase _useCase;

    public EmissionsController(IEmissionsUseCase useCase)
    {
        _useCase = useCase;
    }
}