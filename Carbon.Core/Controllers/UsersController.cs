using Carbon.Application.Contracts;
using Carbon.Application.Dtos.Users;
using Carbon.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carbon.Core.Controllers;

// Área do Admin: ver os usuários cadastrados e trocar o nível de acesso
[ApiController]
[Authorize(Roles = "Admin")]
[Route("Carbon/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersUseCase _useCase;

    public UsersController(IUsersUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpGet]
    public async Task<IReadOnlyList<UserSummaryDto>> GetAll(CancellationToken ct = default)
        => await _useCase.GetAll(ct);

    [HttpPut("{id:guid}/Role")]
    public async Task<UserSummaryDto> ChangeRole(Guid id, ChangeRoleRequestDto request, CancellationToken ct = default)
        => await _useCase.ChangeRole(GetUserId(), id, request, ct);

    private Guid GetUserId()
    {
        var sub = User.FindFirst("sub")?.Value;

        return Guid.TryParse(sub, out var userId)
            ? userId
            : throw new UnauthorizedException("Invalid token: user id not found.");
    }
}
