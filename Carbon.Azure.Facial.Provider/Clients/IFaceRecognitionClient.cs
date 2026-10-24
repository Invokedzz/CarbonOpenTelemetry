namespace Carbon.Azure.Facial.Clients;

public interface IFaceRecognitionClient
{
    FaceEmbeddingResult GetEmbedding(byte[] image);
    float Compare(float[] first, float[] second);
}

public enum FaceEmbeddingStatus
{
    Success,
    InvalidImage,
    NoFaceFound,
    MultipleFacesFound
}

public record FaceEmbeddingResult(FaceEmbeddingStatus Status, float[]? Embedding);