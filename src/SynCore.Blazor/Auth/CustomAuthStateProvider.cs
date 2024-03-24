using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace SynCore.Blazor.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;

    public CustomAuthStateProvider(HttpClient http)
    {
        _httpClient = http;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var res = await _httpClient.GetAsync("/api/auth/me");

        var content = await res.Content.ReadAsStringAsync();
        var claims = JsonSerializer.Deserialize<Dictionary<string, string>>(content);

        ClaimsIdentity identity = new ClaimsIdentity();
        if (claims.Count > 0)
        {
            identity.AddClaims(claims.Select(c => new Claim(c.Key, c.Value)));
        }
        var user = new ClaimsPrincipal(identity);
        
        return new AuthenticationState(user);
    }
}

public class ClaimResponse
{
    public string Identifier { get; set; }
}