using System.ComponentModel.DataAnnotations;

namespace Carbon.Application.Dtos.Authentication.Refresh;

public record RefreshRequestDto(
    [Required(ErrorMessage = "Token is required!", AllowEmptyStrings = false)] 
    string Token);