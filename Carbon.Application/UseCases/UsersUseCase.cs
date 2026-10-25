using Carbon.Application.Contracts;
using Carbon.Application.Dtos.Users;
using Carbon.Domain.Contracts.Services.Users;

namespace Carbon.Application.UseCases;

public class UsersUseCase : IUsersUseCase
{
    private readonly IUsersService _usersService;

    public UsersUseCase(IUsersService usersService)
    {
        _usersService = usersService;
    }

    public async Task<IReadOnlyList<UserSummaryDto>> GetAll(CancellationToken ct = default)
    {
        var users = await _usersService.GetAllAsync(ct);
        return users.Select(ToDto).ToList();
    }

    public async Task<UserSummaryDto> ChangeRole(Guid currentUserId, Guid userId, ChangeRoleRequestDto request, CancellationToken ct = default)
        => ToDto(await _usersService.SetRoleAsync(currentUserId, userId, request.Role, ct));

    private static UserSummaryDto ToDto(UserSummary u)
        => new(u.Id, u.Username, u.Email, u.Role, u.HasFace, u.CreatedAt);
}
