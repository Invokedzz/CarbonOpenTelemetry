using Carbon.Domain.Contracts.Providers.Carbon;
using Carbon.Interface.Clients;

namespace Infrastructure.Providers;

public class CarbonEstimateProvider : ICarbonEstimateProvider
{
    private readonly ICarbonInterfaceClient _client;

    public CarbonEstimateProvider(ICarbonInterfaceClient client)
    {
        _client = client;
    }
    
    public Task<FlightCarbonEstimateResponse> FlightEstimateAsync(FlightCarbonEstimateRequest request, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}