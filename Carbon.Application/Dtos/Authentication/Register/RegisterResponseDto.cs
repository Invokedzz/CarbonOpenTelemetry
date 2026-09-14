namespace Carbon.Application.Dtos.Authentication.Register
{
    public record RegisterResponseDto(UserDto User);

    public record UserDto(string Username, string Email, bool IsActive, DateTime CreatedAt);
}