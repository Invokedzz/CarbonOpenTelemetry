namespace Carbon.Travel.Impact.Provider;

public interface ITravelImpactSession
{
    Task<TRs> SendAsync<TRqs, TRs>(TRqs request, CancellationToken ct);
}