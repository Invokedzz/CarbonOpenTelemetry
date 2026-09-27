namespace Carbon.Domain.Contracts.Services.Emissions;

public interface IEmissionsService
{
    Task<EmissionsCalculationResponse> CalculateAsync(EmissionsCalculationRequest request, CancellationToken ct = default);
}