using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using SupportTicket.Client.Resources;
using SupportTicket.Client.Services;

namespace SupportTicket.Client.Providers;

public class AuthorizedStateProvider(ISessionStorageService sessionStorageService) : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await sessionStorageService.GetItemAsync<string>(Constants.Token);

        if (string.IsNullOrEmpty(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var claims = ParseClaimsFromJwt(token);
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

        return new AuthenticationState(user);
    }

    public void NotifyUserAuthentication(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            var state = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            NotifyAuthenticationStateChanged(state);
            return;
        }

        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes) ?? new();

        keyValuePairs.TryGetValue(ClaimTypes.Role, out object? roles);

        if (roles == null)
        {
            return [];
        }

        if (roles.ToString()?.Trim().StartsWith("[") ?? false)
        {
            var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString() ?? string.Empty);
            foreach (var parsedRole in parsedRoles ?? [])
            {
                claims.Add(new Claim(ClaimTypes.Role, parsedRole));
            }
        }
        else
        {
            claims.Add(new Claim(ClaimTypes.Role, roles.ToString() ?? string.Empty));
        }

        keyValuePairs.Remove(ClaimTypes.Role);
        claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString() ?? string.Empty)));

        return claims;
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }

        return Convert.FromBase64String(base64);
    }
}