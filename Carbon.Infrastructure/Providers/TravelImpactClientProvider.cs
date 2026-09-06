using System.Globalization;
using Carbon.Domain.Contracts.Providers.TravelImpact;
using Carbon.Travel.Impact.Provider.Clients;
using Carbon.Travel.Impact.Provider.Contracts;
using Flight = Carbon.Travel.Impact.Provider.Contracts.Flight;
using FlightContract = Carbon.Domain.Contracts.Providers.TravelImpact.Flight;

namespace Infrastructure.Providers;

public class TravelImpactClientProvider : ITravelImpactProvider
{
    private readonly ITravelImpactClient _client;

    public TravelImpactClientProvider(ITravelImpactClient client)
    {
        _client = client;
    }
    
    public async Task<ImpactProviderResponse> GetFlightEmissionsFromProviderAsync(ImpactProviderRequest request, CancellationToken ct = default)
    {
        var flightEmissionsRequest = new FlightEmissionsRequest
        {
            Flights = request.Flights.Select(flights => new Flight
            {
                Origin = flights.Origin.ToUpperInvariant(),
                Destination = flights.Destination.ToUpperInvariant(),
                FlightNumber = flights.FlightNumber,
                OperatingCarrierCode = flights.CarrierCode.ToUpperInvariant(),
                Date = new DepartureDate
                {
                    Day = flights.DepartureDate.Day,
                    Month = flights.DepartureDate.Month,
                    Year = flights.DepartureDate.Year
                }
            }).ToList()
        };

        var flightEmissionsResponse = await _client.GetFlightEmissionsAsync(flightEmissionsRequest, ct);

        return GetResponse(flightEmissionsResponse);
    }

    private static ImpactProviderResponse GetResponse(FlightEmissionsResponse flightEmissionsResponse)
    {
        var flightsWithEmissions = flightEmissionsResponse.Flights ?? [];
        if (flightsWithEmissions.Count == 0)
        {
            return new ImpactProviderResponse
            {
                Id = Guid.NewGuid(),
                Flights = [],
                Emissions = new EmissionsPerPax
                {
                    First = 0,
                    Business = 0,
                    Economy = 0,
                    PremiumEconomy = 0
                },
                ImpactBucket = ImpactBucket.ContrailsImpactUnspecified,
                Dated = DateOnly.MinValue,
                RequestedAt = DateTime.Now
            };
        }

        var flights = GetFlights(flightsWithEmissions);
        var emissions = GetEmissions(flightsWithEmissions);
        
        return new ImpactProviderResponse
        {
            Id = Guid.NewGuid(),
            Flights = ParseFlights(flights),
            Emissions = ParseEmissions(emissions),
            RequestedAt = DateTimeOffset.UtcNow,
            Dated = ParseDate(flightEmissionsResponse.ModelVersion?.Dated),
            ImpactBucket = ParseImpactBucket(flightsWithEmissions.FirstOrDefault()?.ContrailsImpactBucket)
        };
    }

    private static List<FlightContract> ParseFlights(IEnumerable<Flight> flights)
    {
        return flights.Select(flight => new FlightContract
        {
            Origin = flight.Origin,
            Destination = flight.Destination,
            CarrierCode = flight.OperatingCarrierCode,
            FlightNumber = flight.FlightNumber,
            DepartureDate = ParseDate(HandleDepartureDate(flight.Date))
        }).ToList();
    }

    private static EmissionsPerPax ParseEmissions(List<EmissionsGramsPerPax?> emissions)
    {
        var first = emissions.Sum(e => e?.First) ?? 0;
        var premiumEconomy = emissions.Sum(e => e?.PremiumEconomy) ?? 0;
        var business = emissions.Sum(e => e?.Business) ?? 0;
        var economy = emissions.Sum(e => e?.Economy) ?? 0;
        
        return new EmissionsPerPax
        {
            First = first,
            PremiumEconomy = premiumEconomy,
            Business = business,
            Economy = economy
        };
    }
    
    private static DateOnly ParseDate(string? date)
    {
        const string format = "yyyyMMdd";
        
        if (string.IsNullOrWhiteSpace(date))
            return DateOnly.MinValue;
        
        return DateOnly.ParseExact(date, format, CultureInfo.InvariantCulture);
    }

    private static ImpactBucket ParseImpactBucket(string? bucket)
    {
        if (string.IsNullOrWhiteSpace(bucket))
            return ImpactBucket.ContrailsImpactUnspecified;
        
        return bucket.ToUpperInvariant() switch
        {
            "CONTRAILS_IMPACT_NEGLIGIBLE" => ImpactBucket.ContrailsImpactNegligible,
            "CONTRAILS_IMPACT_MODERATE" => ImpactBucket.ContrailsImpactModerate,
            "CONTRAILS_IMPACT_SEVERE" => ImpactBucket.ContrailsImpactSevere,
            _ => ImpactBucket.ContrailsImpactUnspecified
        };
    }
    
    private static List<Flight> GetFlights(List<FlightWithEmissions> flightsWithEmissions) 
        => flightsWithEmissions
            .Select(e => e.Flight)
            .ToList();

    private static List<EmissionsGramsPerPax?> GetEmissions(List<FlightWithEmissions> flightsWithEmissions)
        => flightsWithEmissions
            .Select(e => e.Details)
            .ToList();

    private static string HandleDepartureDate(DepartureDate departureDate)
    {
        var year = departureDate.Year < DateTime.UtcNow.Year
            ? DateTime.UtcNow.Year
            : departureDate.Year;

        return $"{year:D4}{departureDate.Month:D2}{departureDate.Day:D2}";
    }
}