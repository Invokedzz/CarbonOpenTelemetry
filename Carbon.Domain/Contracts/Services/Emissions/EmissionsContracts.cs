using Carbon.Domain.Contracts.Providers.TravelImpact;

namespace Carbon.Domain.Contracts.Services.Emissions
{
    public enum CabinClass
    {
        Economy,
        PremiumEconomy,
        Business,
        First
    }

    public enum EmissionLevel
    {
        Low,
        Moderate,
        High
    }

    public record FlightLeg(
        string Origin,
        string Destination,
        string CarrierCode,
        int FlightNumber,
        DateOnly DepartureDate);

    public record EmissionsCalculationRequest(
        Guid UserId,
        List<FlightLeg> Flights,
        CabinClass Cabin,
        int Passengers,
        bool RoundTrip);

    public record EmissionsCalculationResponse(
        Guid Id,
        FlightInformation FlightInformation,
        TotalEmissions TotalEmissions,
        DateOnly ModelDate,
        DateTimeOffset CalculatedAt);
    
    public record FlightInformation(
        CabinClass Cabin,
        int Passengers,
        int FlightsCount,
        bool RoundTrip);
    
    public record TotalEmissions(
        decimal KgPerPassenger,
        decimal TotalKg,
        decimal TotalTonnes,
        TotalImpact Impact);
    
    public record TotalImpact(EmissionLevel Level,
        ImpactBucket ContrailsImpact);
    
    public static class EmissionsRules
    {
        public const int MaxFlights = 10;
        public const int MinPassengers = 1;
        public const int MaxPassengers = 100;
        public const decimal LowLimitKgPerPassenger = 100m;
        public const decimal ModerateLimitKgPerPassenger = 500m;
    }
}