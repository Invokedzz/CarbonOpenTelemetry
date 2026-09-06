using Carbon.Domain.Contracts.Providers.TravelImpact;
using Carbon.Travel.Impact.Provider.Clients;
using Carbon.Travel.Impact.Provider.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Carbon.Core.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController: ControllerBase
{
    private readonly ITravelImpactProvider _provider;

    public TestController(ITravelImpactProvider provider)
    {
        _provider = provider;
    }

    [HttpPost]
    public async Task<ImpactProviderResponse> Test(ImpactProviderRequest request,
        CancellationToken ct = default)
    {
        return await _provider.GetFlightEmissionsFromProviderAsync(request, ct);
    }
}