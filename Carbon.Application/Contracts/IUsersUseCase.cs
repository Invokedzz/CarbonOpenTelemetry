using Carbon.Application.Dtos.Users;

namespace Carbon.Application.Contracts;

public interface IUsersUseCase
{
    Task<IReadOnlyList<UserSummaryDto>> GetAll(CancellationToken ct);
    Task<UserSummaryDto> ChangeRole(Guid currentUserId, Guid userId, ChangeRoleRequestDto request, CancellationToken ct);
}
