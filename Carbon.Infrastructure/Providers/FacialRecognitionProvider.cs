using Carbon.Azure.Facial.Clients;
using Carbon.Domain.Contracts.Providers.Facial;
using Carbon.Domain.Exceptions;

namespace Infrastructure.Providers;

public class FacialRecognitionProvider : IFacialRecognitionProvider
{
    private readonly IFaceRecognitionClient _client;

    public FacialRecognitionProvider(IFaceRecognitionClient client)
    {
        _client = client;
    }

    public float[] GetFaceEmbedding(byte[] image)
    {
        var result = _client.GetEmbedding(image);

        return result.Status switch
        {
            FaceEmbeddingStatus.Success when result.Embedding is not null => result.Embedding,
            FaceEmbeddingStatus.InvalidImage => throw new BadRequestException("Invalid image. Send a JPG or PNG photo."),
            FaceEmbeddingStatus.NoFaceFound => throw new BadRequestException("No face found in the image. Try again with better lighting and your face centered."),
            FaceEmbeddingStatus.MultipleFacesFound => throw new BadRequestException("More than one face found in the image. Only one person must appear in the photo."),
            _ => throw new InternalServerError("Could not process the face image.")
        };
    }

    public float Compare(float[] first, float[] second) => _client.Compare(first, second);
}