namespace Carbon.Application.Dtos.Emissions;

public record EmissionsResponseDto(
    Guid Id,
    string Cabin,
    int Passengers,
    bool RoundTrip,
    int FlightsCount,
    decimal KgPerPassenger,
    decimal TotalKg,
    decimal TotalTonnes,
    string Level,
    string ContrailsImpact,
    DateOnly ModelDate,
    DateTimeOffset CalculatedAt);