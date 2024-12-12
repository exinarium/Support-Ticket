namespace SupportTicket.API.Domain.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddSystemServices(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}