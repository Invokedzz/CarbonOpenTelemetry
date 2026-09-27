namespace Carbon.Application.Dtos.Authentication.Login
{
    public record LoginResponseDto(TokenDto Token, DateTime CreatedAt);

    public record TokenDto(string AccessToken, string RefreshToken, DateTime ExpiresAt, bool IsRevoked);

    public record RefreshTokenDto(string RefreshToken, string AccessToken, DateTime ExpiresAt, bool IsRevoked)
        : TokenDto(RefreshToken, AccessToken, ExpiresAt, IsRevoked);
}
