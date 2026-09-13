namespace Carbon.Application.Dtos.Authentication
{
    public record LoginResponseDto(TokenDto Token, DateTime CreatedAt);

    public record TokenDto(string AccessToken, DateTime ExpiresAt, bool IsRevoked);
}
