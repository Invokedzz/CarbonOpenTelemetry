using System.ComponentModel.DataAnnotations;

namespace Carbon.Application.Dtos.Facial;

// Foto em base64. Aceita base64 puro ou "data:image/jpeg;base64,..." (formato da webcam)
public record FaceImageRequestDto(
    [Required(ErrorMessage = "ImageBase64 field is required!", AllowEmptyStrings = false)]
    string ImageBase64);