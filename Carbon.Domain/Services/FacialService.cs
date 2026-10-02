using System.Runtime.InteropServices;
using Carbon.Domain.Contracts.Data;
using Carbon.Domain.Contracts.Providers.Facial;
using Carbon.Domain.Contracts.Services.Facial;
using Carbon.Domain.Exceptions;
using Carbon.Domain.Models;

namespace Carbon.Domain.Services;

public class FacialService : IFacialService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFacialRecognitionProvider _provider;

    public FacialService(IUnitOfWork unitOfWork, IFacialRecognitionProvider provider)
    {
        _unitOfWork = unitOfWork;
        _provider = provider;
    }

    public async Task<FaceRegisterResponse> RegisterFaceAsync(Guid userId, byte[] image, CancellationToken ct = default)
    {
        ValidateImage(image);
        await GetUserAsync(userId, ct);

        var embedding = _provider.GetFaceEmbedding(image);

        await _unitOfWork.UserRepository.UpdateFaceEmbeddingAsync(userId, ToBytes(embedding), ct);

        return new FaceRegisterResponse(userId, DateTime.UtcNow);
    }

    public async Task<FaceVerificationResponse> VerifyFaceAsync(Guid userId, byte[] image, CancellationToken ct = default)
    {
        ValidateImage(image);
        var user = await GetUserAsync(userId, ct);

        if (user.FaceEmbedding is null || user.FaceEmbedding.Length == 0)
        {
            throw new BadRequestException("User has no registered face. Register a face before verifying.");
        }

        var storedEmbedding = ToFloats(user.FaceEmbedding);
        var currentEmbedding = _provider.GetFaceEmbedding(image);

        var similarity = _provider.Compare(storedEmbedding, currentEmbedding);
        var isMatch = similarity >= FacialRules.MatchThreshold;

        return new FaceVerificationResponse(userId, isMatch, MathF.Round(similarity, 4), DateTime.UtcNow);
    }

    private async Task<User> GetUserAsync(Guid userId, CancellationToken ct)
        => await _unitOfWork.UserRepository.GetByIdAsync(userId, ct)
           ?? throw new NotFoundException($"User with id: {userId} not found!");

    private static void ValidateImage(byte[] image)
    {
        if (image is null || image.Length == 0)
        {
            throw new BadRequestException("Image is required.");
        }

        if (image.Length > FacialRules.MaxImageBytes)
        {
            throw new BadRequestException($"Image must have at most {FacialRules.MaxImageBytes / (1024 * 1024)} MB.");
        }
    }

    // O embedding (512 floats) é guardado no banco como bytes
    private static byte[] ToBytes(float[] embedding)
        => MemoryMarshal.AsBytes(embedding.AsSpan()).ToArray();

    private static float[] ToFloats(byte[] bytes)
        => MemoryMarshal.Cast<byte, float>(bytes.AsSpan()).ToArray();
}