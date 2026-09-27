using Carbon.Application.Contracts;
using Carbon.Application.Dtos.Emissions;
using Carbon.Domain.Contracts.Services.Emissions;

namespace Carbon.Application.UseCases;

public class EmissionsUseCase : IEmissionsUseCase
{
    private readonly IEmissionsService _emissionsService;

    public EmissionsUseCase(IEmissionsService emissionsService)
    {
        _emissionsService = emissionsService;
    }

    public async Task<EmissionsResponseDto> Calculate(EmissionsRequestDto request, CancellationToken ct = default)
    {
        var calculationRequest = new EmissionsCalculationRequest(
            request.Flights.Select(f => new FlightLeg(
                f.Origin,
                f.Destination,
                f.CarrierCode,
                f.FlightNumber,
                f.DepartureDate)).ToList(),
            request.Cabin,
            request.Passengers,
            request.RoundTrip);

        var result = await _emissionsService.CalculateAsync(calculationRequest, ct);

        return new EmissionsResponseDto(
            result.Id,
            result.Cabin.ToString(),
            result.Passengers,
            result.RoundTrip,
            result.FlightsCount,
            result.KgPerPassenger,
            result.TotalKg,
            result.TotalTonnes,
            result.Level.ToString(),
            result.ContrailsImpact.ToString(),
            result.ModelDate,
            result.CalculatedAt);
    }
}