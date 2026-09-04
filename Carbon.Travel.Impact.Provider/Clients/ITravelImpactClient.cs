using Carbon.Travel.Impact.Provider.Contracts;

namespace Carbon.Travel.Impact.Provider.Clients;

public interface ITravelImpactClient
{
    Task<DetailedFlightEmissionsResponse> GetDetailedFlightEmissionsAsync(DetailedFlightEmissionsRequest request, CancellationToken ct);
}