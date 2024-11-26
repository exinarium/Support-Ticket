namespace SupportTicket.API.Domain.Config;

public class SendGridConfig
{
    public string SendGridPostMailURL { get; set; }

    public string SendGridGetTemplateURL { get; set; }

    public string SendGridPublicId{ get; set; }

    public string SendGridTemplateId { get; set; }
    
    public string SendGridApiKey { get; set; }
}