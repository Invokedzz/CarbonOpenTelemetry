using System.Text;
using System.Text.Json;

namespace Carbon.Travel.Impact.Provider;

public class TravelImpactSession : ITravelImpactSession
{
    private const string ClientName = "TravelImpact";
    private const string DefaultCarbonTypeEncoding = "application/json";
    
    private readonly IHttpClientFactory _clientFactory;

    public TravelImpactSession(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<TRs> SendAsync<TRqs, TRs>(TRqs request, CancellationToken ct = default)
    {
        var client = _clientFactory.CreateClient(ClientName);
        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, client.BaseAddress);

        var requestBody = JsonSerializer.Serialize(request);
        requestMessage.Content = new StringContent(requestBody, Encoding.UTF8, DefaultCarbonTypeEncoding);
        
        using var responseMessage = await client.SendAsync(requestMessage, ct);
        var content = await responseMessage.Content.ReadAsStringAsync(ct);

        return JsonSerializer.Deserialize<TRs>(content)!;
    }
}