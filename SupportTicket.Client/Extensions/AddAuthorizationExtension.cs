using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using SupportTicket.Client.Providers;
using SupportTicket.Client.Services;

namespace SupportTicket.Client.Extensions;

public static class AddAuthorizationExtension
{
    public static IServiceCollection AddAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationCore();
        services.AddSingleton<ISessionStorageService, SessionStorageService>();
        services.AddScoped<IAuthorizationPolicyProvider, DefaultAuthorizationPolicyProvider>();
        services.AddScoped<AuthenticationStateProvider, AuthorizedStateProvider>();
        services.AddScoped<IAuthorizationService, DefaultAuthorizationService>();
        services.AddScoped<IAuthorizationHandlerProvider, DefaultAuthorizationHandlerProvider>();
        services.AddScoped<IAuthorizationHandlerContextFactory, DefaultAuthorizationHandlerContextFactory>();
        services.AddScoped<IAuthorizationEvaluator, DefaultAuthorizationEvaluator>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}