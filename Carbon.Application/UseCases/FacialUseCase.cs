using Carbon.Application.Contracts;
using Carbon.Application.Dtos.Facial;
using Carbon.Domain.Contracts.Services.Facial;
using Carbon.Domain.Exceptions;

namespace Carbon.Application.UseCases;

public class FacialUseCase : IFacialUseCase
{
    private readonly IFacialService _facialService;

    public FacialUseCase(IFacialService facialService)
    {
        _facialService = facialService;
    }

    public async Task<FaceRegisterResponseDto> Register(Guid userId, FaceImageRequestDto request, CancellationToken ct = default)
    {
        var image = DecodeImage(request.ImageBase64);
        var result = await _facialService.RegisterFaceAsync(userId, image, ct);

        return new FaceRegisterResponseDto(result.UserId, result.RegisteredAt);
    }

    public async Task<FaceVerificationResponseDto> Verify(Guid userId, FaceImageRequestDto request, CancellationToken ct = default)
    {
        var image = DecodeImage(request.ImageBase64);
        var result = await _facialService.VerifyFaceAsync(userId, image, ct);

        return new FaceVerificationResponseDto(result.UserId, result.IsMatch, result.Similarity, result.VerifiedAt);
    }

    private static byte[] DecodeImage(string imageBase64)
    {
        if (string.IsNullOrWhiteSpace(imageBase64))
        {
            throw new BadRequestException("Image is required.");
        }

        // Remove o prefixo "data:image/...;base64," se vier da webcam
        var commaIndex = imageBase64.IndexOf(',');
        var base64 = imageBase64.StartsWith("data:", StringComparison.OrdinalIgnoreCase) && commaIndex >= 0
            ? imageBase64[(commaIndex + 1)..]
            : imageBase64;

        try
        {
            return Convert.FromBase64String(base64.Trim());
        }
        catch (FormatException)
        {
            throw new BadRequestException("Image must be a valid base64 string.");
        }
    }
}