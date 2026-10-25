using System.ComponentModel.DataAnnotations;

namespace Carbon.Application.Dtos.Users;

public record UserSummaryDto(Guid Id, string Username, string Email, string Role, bool HasFace, DateTime CreatedAt);

public record ChangeRoleRequestDto(
    [Required(ErrorMessage = "Role field is required!", AllowEmptyStrings = false)]
    string Role);
