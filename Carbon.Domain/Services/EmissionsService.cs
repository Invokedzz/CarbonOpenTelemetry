using Carbon.Domain.Contracts.Providers.TravelImpact;
using Carbon.Domain.Contracts.Services.Emissions;
using Carbon.Domain.Exceptions;

namespace Carbon.Domain.Services;

public class EmissionsService : IEmissionsService
{
    private const decimal GramsPerKg = 1_000m;
    private const decimal KgPerTonne = 1_000m;

    private readonly ITravelImpactProvider _provider;

    public EmissionsService(ITravelImpactProvider provider)
    {
        _provider = provider;
    }

    public async Task<EmissionsCalculationResponse> CalculateAsync(EmissionsCalculationRequest request, CancellationToken ct = default)
    {
        Validate(request);

        var providerRequest = new ImpactProviderRequest
        {
            Flights = request.Flights.Select(leg => new Flight
            {
                Origin = leg.Origin.Trim(),
                Destination = leg.Destination.Trim(),
                CarrierCode = leg.CarrierCode.Trim(),
                FlightNumber = leg.FlightNumber,
                DepartureDate = leg.DepartureDate
            }).ToList()
        };

        var providerResponse = await _provider.GetFlightEmissionsFromProviderAsync(providerRequest, ct);

        if (providerResponse.Flights.Count == 0)
        {
            throw new NotFoundException("No emissions data found for the informed flights. Check airports, carrier and flight number.");
        }

        var gramsPerPassenger = GetGramsForCabin(providerResponse.Emissions, request.Cabin);

        if (gramsPerPassenger <= 0)
        {
            throw new BadRequestException($"No emissions data for cabin {request.Cabin} on the informed flights. The cabin may not exist on this aircraft.");
        }

        var tripMultiplier = request.RoundTrip ? 2 : 1;

        var kgPerPassenger = gramsPerPassenger / GramsPerKg * tripMultiplier;
        var totalKg = kgPerPassenger * request.Passengers;

        return new EmissionsCalculationResponse(
            Id: providerResponse.Id,
            Cabin: request.Cabin,
            Passengers: request.Passengers,
            RoundTrip: request.RoundTrip,
            FlightsCount: providerResponse.Flights.Count,
            KgPerPassenger: Math.Round(kgPerPassenger, 2),
            TotalKg: Math.Round(totalKg, 2),
            TotalTonnes: Math.Round(totalKg / KgPerTonne, 3),
            Level: Classify(kgPerPassenger),
            ContrailsImpact: providerResponse.ImpactBucket,
            ModelDate: providerResponse.Dated,
            CalculatedAt: DateTimeOffset.UtcNow);
    }

    private static void Validate(EmissionsCalculationRequest request)
    {
        if (request.Flights is null || request.Flights.Count == 0)
        {
            throw new BadRequestException("At least one flight must be informed.");
        }

        if (request.Flights.Count > EmissionsRules.MaxFlights)
        {
            throw new BadRequestException($"A maximum of {EmissionsRules.MaxFlights} flights can be calculated at once.");
        }

        if (request.Passengers is < EmissionsRules.MinPassengers or > EmissionsRules.MaxPassengers)
        {
            throw new BadRequestException($"Passengers must be between {EmissionsRules.MinPassengers} and {EmissionsRules.MaxPassengers}.");
        }

        foreach (var leg in request.Flights)
        {
            if (!IsIataAirport(leg.Origin) || !IsIataAirport(leg.Destination))
            {
                throw new BadRequestException($"Invalid airport code on flight {leg.CarrierCode}{leg.FlightNumber}. Use the 3-letter IATA code (e.g. GRU).");
            }

            if (string.Equals(leg.Origin.Trim(), leg.Destination.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                throw new BadRequestException("Origin and destination must be different.");
            }

            if (string.IsNullOrWhiteSpace(leg.CarrierCode) || leg.CarrierCode.Trim().Length != 2)
            {
                throw new BadRequestException("Carrier code must have 2 characters (e.g. LA, G3, AD).");
            }

            if (leg.FlightNumber <= 0)
            {
                throw new BadRequestException("Flight number must be greater than zero.");
            }

            if (leg.DepartureDate == default)
            {
                throw new BadRequestException("Departure date is required.");
            }
        }
    }

    private static bool IsIataAirport(string? code)
        => !string.IsNullOrWhiteSpace(code)
           && code.Trim().Length == 3
           && code.Trim().All(char.IsLetter);

    private static decimal GetGramsForCabin(EmissionsPerPax emissions, CabinClass cabin)
        => cabin switch
        {
            CabinClass.Economy => emissions.Economy,
            CabinClass.PremiumEconomy => emissions.PremiumEconomy,
            CabinClass.Business => emissions.Business,
            CabinClass.First => emissions.First,
            _ => throw new BadRequestException($"Unknown cabin class: {cabin}")
        };

    private static EmissionLevel Classify(decimal kgPerPassenger)
        => kgPerPassenger switch
        {
            < EmissionsRules.LowLimitKgPerPassenger => EmissionLevel.Low,
            < EmissionsRules.ModerateLimitKgPerPassenger => EmissionLevel.Moderate,
            _ => EmissionLevel.High
        };
}