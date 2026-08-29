using Carbon.Interface.Models.Flight.Request;
using Carbon.Interface.Models.Flight.Response;

namespace Carbon.Interface.Clients;

public class CarbonInterfaceClient : ICarbonInterfaceClient
{
    private readonly ICarbonInterfaceSession _session;

    public CarbonInterfaceClient(ICarbonInterfaceSession session)
    {
        _session = session;
    }
    
    public async Task<FlightEstimateResponse> FlightEstimateAsync(FlightEstimateRequest request, CancellationToken ct = default)
    {
        return await _session.SendAsync<FlightEstimateRequest, FlightEstimateResponse>
            (request, $"", ct);
    }
}