using System.Net;
using System.Net.Http.Headers;

namespace Carbon.Front.Util;

public sealed class ApiClient(HttpClient http, SessionStore session)
{
    public async Task<HttpResponseMessage> PostAsync<T>(string path, T body, bool authorized = false, CancellationToken ct = default)
    {
        using var req = new HttpRequestMessage(HttpMethod.Post, path) { Content = JsonContent.Create(body) };
 
        if (authorized)
        {
            await session.InitializeAsync();
            if (!string.IsNullOrEmpty(session.AccessToken))
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", session.AccessToken);
        }
 
        var res = await http.SendAsync(req, ct);
        if (authorized && res.StatusCode == HttpStatusCode.Unauthorized)
            await session.ClearAsync();
        return res;
    }
}