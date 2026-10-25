using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace Carbon.Front.Util;

// Lê o token salvo no SessionStore e transforma em usuário logado (com cargos) para o Blazor.
public sealed class JwtAuthStateProvider : AuthenticationStateProvider, IDisposable
{
    private static readonly AuthenticationState Anonymous = new(new ClaimsPrincipal(new ClaimsIdentity()));
    private readonly SessionStore _session;

    public JwtAuthStateProvider(SessionStore session)
    {
        _session = session;
        _session.Changed += OnSessionChanged;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        await _session.InitializeAsync();
        var token = _session.AccessToken;
        if (string.IsNullOrEmpty(token)) return Anonymous;

        var claims = ReadClaims(token);
        if (claims is null) return Anonymous;

        var exp = claims.FirstOrDefault(c => c.Type == "exp")?.Value;
        if (long.TryParse(exp, out var seconds) && DateTimeOffset.FromUnixTimeSeconds(seconds) <= DateTimeOffset.UtcNow)
            return Anonymous;

        var identity = new ClaimsIdentity(claims, "jwt", "name", ClaimTypes.Role);
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    private static List<Claim>? ReadClaims(string token)
    {
        var parts = token.Split('.');
        if (parts.Length < 2) return null;

        try
        {
            var payload = parts[1].Replace('-', '+').Replace('_', '/');
            payload = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
            using var doc = JsonDocument.Parse(Encoding.UTF8.GetString(Convert.FromBase64String(payload)));

            var claims = new List<Claim>();
            foreach (var prop in doc.RootElement.EnumerateObject())
            {
                var type = prop.Name is "role" or "roles" ? ClaimTypes.Role : prop.Name;
                if (prop.Value.ValueKind == JsonValueKind.Array)
                    claims.AddRange(prop.Value.EnumerateArray().Select(v => new Claim(type, v.ToString())));
                else
                    claims.Add(new Claim(type, prop.Value.ToString()));
            }
            return claims;
        }
        catch (Exception e) when (e is FormatException or JsonException)
        {
            return null;
        }
    }

    private void OnSessionChanged() => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

    public void Dispose() => _session.Changed -= OnSessionChanged;
}
