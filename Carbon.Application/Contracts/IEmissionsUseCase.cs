using Carbon.Application.Dtos.Emissions;

namespace Carbon.Application.Contracts;

public interface IEmissionsUseCase
{
    Task<EmissionsResponseDto> Calculate(EmissionsRequestDto request, CancellationToken ct);
}