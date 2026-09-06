using Carbon.Travel.Impact.Provider.Contracts;

namespace Carbon.Travel.Impact.Provider.Clients;

public interface ITravelImpactClient
{
    Task<FlightEmissionsResponse> GetFlightEmissionsAsync(FlightEmissionsRequest request, CancellationToken ct);
}