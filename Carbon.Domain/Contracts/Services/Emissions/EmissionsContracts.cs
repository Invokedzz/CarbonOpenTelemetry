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
        List<FlightLeg> Flights,
        CabinClass Cabin,
        int Passengers,
        bool RoundTrip);

    public record EmissionsCalculationResponse(
        Guid Id,
        CabinClass Cabin,
        int Passengers,
        bool RoundTrip,
        int FlightsCount,
        decimal KgPerPassenger,
        decimal TotalKg,
        decimal TotalTonnes,
        EmissionLevel Level,
        ImpactBucket ContrailsImpact,
        DateOnly ModelDate,
        DateTimeOffset CalculatedAt);

    /// <summary>
    /// Regras de negócio do cálculo. Os limites são de referência e podem ser ajustados pelo grupo.
    /// </summary>
    public static class EmissionsRules
    {
        public const int MaxFlights = 10;
        public const int MinPassengers = 1;
        public const int MaxPassengers = 100;

        // Classificação por passageiro (kg de CO2e na viagem inteira)
        public const decimal LowLimitKgPerPassenger = 100m;
        public const decimal ModerateLimitKgPerPassenger = 500m;
    }
}