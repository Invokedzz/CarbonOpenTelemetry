using Carbon.Domain.Contracts.Providers.TravelImpact;
using Carbon.Domain.Contracts.Services.Emissions;

namespace Carbon.Domain.Services;

public class EmissionsService : IEmissionsService
{
    private readonly ITravelImpactProvider _provider;

    public EmissionsService(ITravelImpactProvider provider)
    {
        _provider = provider;
    }
}