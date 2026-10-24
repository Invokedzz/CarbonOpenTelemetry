using Carbon.Application.Dtos.Facial;

namespace Carbon.Application.Contracts;

public interface IFacialUseCase
{
    Task<FaceRegisterResponseDto> Register(Guid userId, FaceImageRequestDto request, CancellationToken ct);
    Task<FaceVerificationResponseDto> Verify(Guid userId, FaceImageRequestDto request, CancellationToken ct);
}