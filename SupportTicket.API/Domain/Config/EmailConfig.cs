namespace SupportTicket.API.Domain.Config;

public class EmailConfig
{
    public string Username { get; set; }

    public string Password { get; set; }

    public string Host { get; set; }

    public int Port { get; set; }

    public string FromAddress { get; set; }

    public string FromName { get; set; }
    
    public bool EnableSSL { get; set; }
}