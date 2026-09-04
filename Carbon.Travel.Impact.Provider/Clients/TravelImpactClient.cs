using Carbon.Travel.Impact.Provider.Contracts;

namespace Carbon.Travel.Impact.Provider.Clients;

public class TravelImpactClient : ITravelImpactClient
{
    private readonly ITravelImpactSession _impactSession;
    
    public TravelImpactClient(ITravelImpactSession impactSession)
    {
        _impactSession = impactSession;
    }
    
    public async Task<DetailedFlightEmissionsResponse> GetDetailedFlightEmissionsAsync(DetailedFlightEmissionsRequest request, CancellationToken ct = default)
        => await _impactSession.SendAsync<DetailedFlightEmissionsRequest, DetailedFlightEmissionsResponse>(request, ct);
}