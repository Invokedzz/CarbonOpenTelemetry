namespace Carbon.Interface;

public interface ICarbonInterfaceSession
{
    Task<TRs> SendAsync<TRqs, TRs>(TRqs request, string url, CancellationToken ct);
}