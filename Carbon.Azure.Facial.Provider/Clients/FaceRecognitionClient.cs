using FaceAiSharp;
using FaceAiSharp.Extensions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Carbon.Azure.Facial.Clients;

// Reconhecimento facial local com a FaceAiSharp. Registrar como Singleton (modelos pesados).
public sealed class FaceRecognitionClient : IFaceRecognitionClient, IDisposable
{
    private readonly IFaceDetectorWithLandmarks _detector;
    private readonly IFaceEmbeddingsGenerator _generator;
    private readonly object _lock = new();

    public FaceRecognitionClient()
    {
        _detector = FaceAiSharpBundleFactory.CreateFaceDetectorWithLandmarks();
        _generator = FaceAiSharpBundleFactory.CreateFaceEmbeddingsGenerator();
    }

    public FaceEmbeddingResult GetEmbedding(byte[] image)
    {
        Image<Rgb24> img;

        try
        {
            img = Image.Load<Rgb24>(image);
        }
        catch (ImageFormatException)
        {
            return new FaceEmbeddingResult(FaceEmbeddingStatus.InvalidImage, null);
        }

        using (img)
        {
            lock (_lock)
            {
                var faces = _detector.DetectFaces(img);

                if (faces.Count == 0)
                    return new FaceEmbeddingResult(FaceEmbeddingStatus.NoFaceFound, null);

                if (faces.Count > 1)
                    return new FaceEmbeddingResult(FaceEmbeddingStatus.MultipleFacesFound, null);

                var face = faces.First();

                if (face.Landmarks is null)
                    return new FaceEmbeddingResult(FaceEmbeddingStatus.NoFaceFound, null);

                _generator.AlignFaceUsingLandmarks(img, face.Landmarks);
                var embedding = _generator.GenerateEmbedding(img);

                return new FaceEmbeddingResult(FaceEmbeddingStatus.Success, embedding);
            }
        }
    }

    public float Compare(float[] first, float[] second) => first.Dot(second);

    public void Dispose()
    {
        (_detector as IDisposable)?.Dispose();
        (_generator as IDisposable)?.Dispose();
    }
}