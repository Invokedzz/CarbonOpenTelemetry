namespace Carbon.Application.Dtos.Authentication
{
    public record RegisterResponseDto(UserDto User);

    public record UserDto(string Username, string Email, bool IsActive, DateTime CreatedAt);
}