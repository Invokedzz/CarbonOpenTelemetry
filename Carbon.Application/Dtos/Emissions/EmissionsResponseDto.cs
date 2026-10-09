namespace Carbon.Application.Dtos.Emissions
{
    public record EmissionsResponseDto(
        Guid Id,
        FlightInformation Information,
        TotalEmissions TotalEmissions,
        DateOnly ModelDate,
        DateTimeOffset CalculatedAt);

    public record FlightInformation(
        string Cabin,
        int Passengers,
        int FlightsCount,
        bool RoundTrip);
    
    public record TotalEmissions(
        decimal KgPerPassenger,
        decimal TotalKg,
        decimal TotalTonnes,
        TotalImpact Impact);
    
    public record TotalImpact(string Level,
        string ContrailsImpact);
}
    
    