namespace Carbon.Application.Dtos.Authentication.Login
{
    public record LoginResponseDto(TokenDto Token, DateTime CreatedAt);

    public record TokenDto(string AccessToken, DateTime ExpiresAt, bool IsRevoked);
}
