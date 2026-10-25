using System.Net;
using System.Net.Http.Headers;

namespace Carbon.Front.Util;

public sealed class ApiClient(HttpClient http, SessionStore session)
{
    public Task<HttpResponseMessage> PostAsync<T>(string path, T body, bool authorized = false, CancellationToken ct = default)
        => SendAsync(new HttpRequestMessage(HttpMethod.Post, path) { Content = JsonContent.Create(body) }, authorized, ct);

    public Task<HttpResponseMessage> PutAsync<T>(string path, T body, bool authorized = false, CancellationToken ct = default)
        => SendAsync(new HttpRequestMessage(HttpMethod.Put, path) { Content = JsonContent.Create(body) }, authorized, ct);

    public Task<HttpResponseMessage> GetAsync(string path, bool authorized = false, CancellationToken ct = default)
        => SendAsync(new HttpRequestMessage(HttpMethod.Get, path), authorized, ct);

    private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage req, bool authorized, CancellationToken ct)
    {
        using (req)
        {
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
}
