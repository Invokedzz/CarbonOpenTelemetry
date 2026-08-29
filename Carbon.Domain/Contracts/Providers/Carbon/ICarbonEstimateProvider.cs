namespace Carbon.Domain.Contracts.Providers.Carbon;

public interface ICarbonEstimateProvider
{
    Task<FlightCarbonEstimateResponse> FlightEstimateAsync(FlightCarbonEstimateRequest request, CancellationToken ct);
}