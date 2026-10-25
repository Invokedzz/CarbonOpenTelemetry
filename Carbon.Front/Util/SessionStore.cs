using System.Security.Cryptography;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Carbon.Front.Util;

public sealed class SessionStore(ProtectedLocalStorage storage)
{
    private const string Key = "carbon.session";
    private bool _loaded;
 
    public string? AccessToken { get; private set; }
    public string? RefreshToken { get; private set; }
    public bool FaceVerified { get; private set; }
    public event Action? Changed;
 
    private sealed record StoredTokens(string AccessToken, string? RefreshToken, bool FaceVerified = false);
 
    public async Task InitializeAsync()
    {
        if (_loaded) return;
        try
        {
            var stored = await storage.GetAsync<StoredTokens>(Key);
            if (stored.Success && stored.Value is { } t)
            {
                AccessToken = t.AccessToken;
                RefreshToken = t.RefreshToken;
                FaceVerified = t.FaceVerified;
            }
            _loaded = true;
        }
        catch (InvalidOperationException) { /* prerendering: JS interop unavailable yet, retry later */ }
        catch (CryptographicException) { _loaded = true; /* stale/unreadable payload: treat as signed out */ }
    }
 
    public async Task SetAsync(string accessToken, string? refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        FaceVerified = false;
        _loaded = true;
        await storage.SetAsync(Key, new StoredTokens(accessToken, refreshToken));
        Changed?.Invoke();
    }

    // Chamado depois que o rosto foi conferido (ou cadastrado) com sucesso
    public async Task MarkFaceVerifiedAsync()
    {
        if (string.IsNullOrEmpty(AccessToken)) return;
        FaceVerified = true;
        await storage.SetAsync(Key, new StoredTokens(AccessToken, RefreshToken, true));
        Changed?.Invoke();
    }
 
    public async Task ClearAsync()
    {
        AccessToken = null;
        RefreshToken = null;
        FaceVerified = false;
        try { await storage.DeleteAsync(Key); } catch (InvalidOperationException) { }
        Changed?.Invoke();
    }
}