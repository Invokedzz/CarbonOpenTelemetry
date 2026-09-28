namespace Carbon.Domain.Contracts.Services.Facial;

public interface IFacialService
{
    Task<FaceRegisterResponse> RegisterFaceAsync(Guid userId, byte[] image, CancellationToken ct = default);
    Task<FaceVerificationResponse> VerifyFaceAsync(Guid userId, byte[] image, CancellationToken ct = default);
}