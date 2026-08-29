using System.Text;
using System.Text.Json;

namespace Carbon.Interface;

public class CarbonInterfaceSession : ICarbonInterfaceSession
{
    private const string ClientName = "Carbon";
    private const string DefaultCarbonTypeEncoding = "application/json";
    
    private readonly IHttpClientFactory _clientFactory;

    public CarbonInterfaceSession(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<TRs> SendAsync<TRqs, TRs>(TRqs request, string url, CancellationToken ct = default)
    {
        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

        var requestBody = JsonSerializer.Serialize(request);
        requestMessage.Content = new StringContent(requestBody, Encoding.UTF8, DefaultCarbonTypeEncoding);
        
        var client = _clientFactory.CreateClient(ClientName);
        
        using var responseMessage = await client.SendAsync(requestMessage, ct);
        var content = await responseMessage.Content.ReadAsStringAsync(ct);

        return JsonSerializer.Deserialize<TRs>(content)!;
    }
}