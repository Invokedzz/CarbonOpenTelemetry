namespace Carbon.Application.Dtos.Facial;

public record FaceRegisterResponseDto(Guid UserId, DateTime RegisteredAt);

public record FaceVerificationResponseDto(Guid UserId, bool IsMatch, float Similarity, DateTime VerifiedAt);