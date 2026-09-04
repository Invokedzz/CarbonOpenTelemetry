using Carbon.Domain.Contracts.Providers.TravelImpact;
using Carbon.Travel.Impact.Provider.Clients;
using Carbon.Travel.Impact.Provider.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Carbon.Core.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController: ControllerBase
{
    private readonly ITravelImpactClient _provider;

    public TestController(ITravelImpactClient provider)
    {
        _provider = provider;
    }

    [HttpPost]
    public async Task<DetailedFlightEmissionsResponse> Test(DetailedFlightEmissionsRequest request,
        CancellationToken ct = default)
    {
        return await _provider.GetDetailedFlightEmissionsAsync(request, ct);
    }
}