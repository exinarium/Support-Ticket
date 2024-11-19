using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using SupportTicket.Client.Config;
using SupportTicket.Client.Resources;
using SupportTicket.Client.Services;

namespace SupportTicket.Client.Extensions;

public static class AddHttpClientFactoryExtension
{
    public static IServiceCollection AddHttpClientFactory(this IServiceCollection services)
    {
        services.AddScoped<AuthorizationMessageHandler>();

        services.AddHttpClient(Constants.HttpClientFactoryDefaultName, (provider,client) =>
        {
            var config = provider.GetRequiredService<IOptions<ClientConfig>>();
            client.BaseAddress = new Uri(config.Value.BaseUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        })
        .AddHttpMessageHandler<AuthorizationMessageHandler>();

        return services;
    }
}

public class AuthorizationMessageHandler(ISessionStorageService sessionStorageService) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await sessionStorageService.GetItemAsync<string>(Constants.Token);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }
}