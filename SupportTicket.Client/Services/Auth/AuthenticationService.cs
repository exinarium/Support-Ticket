using System.Net.Http.Json;
using System.Text.Json;
using SupportTicket.Client.Resources;
using SupportTicket.SDK.Models;
using SupportTicket.SDK.Models.Requests.Auth;

namespace SupportTicket.Client.Services;

public interface IAuthenticationService
{
    Task<AuthResult> Login(string email, string password);
}

public class AuthenticationService(IHttpClientFactory httpClientFactory) : IAuthenticationService
{
    public async Task<AuthResult> Login(string email, string password)
    {
        try
        {
            var client = httpClientFactory.CreateClient(Constants.HttpClientFactoryDefaultName);
            var loginRequest = new LoginRequest(email, password);

            var response = await client.PostAsJsonAsync("/api/v1/auth/login", loginRequest);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadFromJsonAsync<AuthResult>();
            return content;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new AuthResult(null, false, e.Message);
        }
    }
}