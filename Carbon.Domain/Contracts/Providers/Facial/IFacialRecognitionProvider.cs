namespace Carbon.Domain.Contracts.Providers.Facial;

public interface IFacialRecognitionProvider
{
    float[] GetFaceEmbedding(byte[] image);
    float Compare(float[] first, float[] second);
}