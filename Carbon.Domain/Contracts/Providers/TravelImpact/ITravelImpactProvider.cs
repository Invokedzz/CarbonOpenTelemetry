namespace Carbon.Domain.Contracts.Providers.TravelImpact;

public interface ITravelImpactProvider
{
    Task<ImpactProviderResponse> GetFlightEmissionsFromProviderAsync(ImpactProviderRequest request, CancellationToken ct);
}