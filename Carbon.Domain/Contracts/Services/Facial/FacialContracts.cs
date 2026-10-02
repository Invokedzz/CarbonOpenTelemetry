namespace Carbon.Domain.Contracts.Services.Facial
{
    public record FaceRegisterResponse(Guid UserId, DateTime RegisteredAt);

    public record FaceVerificationResponse(Guid UserId, bool IsMatch, float Similarity, DateTime VerifiedAt);

    public static class FacialRules
    {
        // Similaridade mínima para considerar a mesma pessoa (recomendação da FaceAiSharp)
        public const float MatchThreshold = 0.42f;

        // Tamanho máximo da foto (5 MB)
        public const int MaxImageBytes = 5 * 1024 * 1024;
    }
}