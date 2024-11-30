namespace SupportTicket.SDK.Models.Requests.Auth;

public record PasswordResetRequest(string Token, string Password);