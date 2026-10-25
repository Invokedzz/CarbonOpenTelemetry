using Carbon.Domain.Contracts.Data;
using Carbon.Domain.Contracts.Services.Authentication;
using Carbon.Domain.Contracts.Services.Users;
using Carbon.Domain.Exceptions;
using Carbon.Domain.Models;

namespace Carbon.Domain.Services;

// Gestão de usuários pelo Admin: listar usuários e trocar o nível de acesso
public class UsersService : IUsersService
{
    // Do maior para o menor nível de acesso
    private static readonly string[] RoleOrder = [nameof(Roles.Admin), nameof(Roles.Director), nameof(Roles.General)];

    private readonly IUnitOfWork _unitOfWork;

    public UsersService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<UserSummary>> GetAllAsync(CancellationToken ct = default)
    {
        var users = await _unitOfWork.UserRepository.GetAllWithRolesAsync(ct);
        return users.Select(ToSummary).ToList();
    }

    public async Task<UserSummary> SetRoleAsync(Guid currentUserId, Guid userId, string role, CancellationToken ct = default)
    {
        var roleName = NormalizeRole(role);

        if (userId == currentUserId && roleName != nameof(Roles.Admin))
            throw new BadRequestException("You can't remove your own Admin role.");

        var user = await _unitOfWork.UserRepository.GetByIdForUpdateAsync(userId, ct)
                   ?? throw new NotFoundException($"User with id: {userId} not found!");

        await ApplyRoleAsync(user, roleName, ct);
        return ToSummary(user);
    }

    public async Task<bool> SetRoleByEmailAsync(string email, string role, CancellationToken ct = default)
    {
        var user = await _unitOfWork.UserRepository.GetByEmailAsync(email, ct);
        if (user is null) return false;

        await ApplyRoleAsync(user, NormalizeRole(role), ct);
        return true;
    }

    private async Task ApplyRoleAsync(User user, string roleName, CancellationToken ct)
    {
        if (user.Roles.Count == 1 && user.Roles.First().Name == roleName)
            return;

        var role = await _unitOfWork.RoleRepository.GetByNameAsync(roleName, ct);
        user.Roles.Clear();
        user.Roles.Add(role);
        await _unitOfWork.SaveChangesAsync(ct);
    }

    private static string NormalizeRole(string role)
        => Enum.GetNames<Roles>().FirstOrDefault(e => string.Equals(e, role?.Trim(), StringComparison.OrdinalIgnoreCase))
           ?? throw new BadRequestException("Role must be General, Director or Admin.");

    private static UserSummary ToSummary(User user)
        => new(
            user.Id,
            user.Username,
            user.Email,
            RoleOrder.FirstOrDefault(r => user.Roles.Any(e => e.Name == r)) ?? nameof(Roles.General),
            user.FaceEmbedding is { Length: > 0 },
            user.CreatedAt);
}
