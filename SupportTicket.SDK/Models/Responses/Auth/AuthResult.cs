namespace SupportTicket.SDK.Models;

public record AuthResult(string Token, bool Success, string ErrorMessage = "");