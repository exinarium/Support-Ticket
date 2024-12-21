namespace SupportTicket.API.Domain.Services.Email;

public interface IEmailService
{
    Task ExecuteSendEmail(string emailId);

    Task SendEmailAsync(Repository.Models.Email email);
}

public class EmailService(
    ILogger<EmailService> logger,
    IDbContextFactory<DataContext> contextFactory,
    IHttpClientFactory httpClientFactory,
    IOptions<EmailConfig> emailConfig,
    IOptions<SendGridConfig> sendGridConfig) :  ServiceBase(contextFactory), IEmailService
{
    public async Task SendEmailAsync(Repository.Models.Email email)
    {
        var savedMail = Context.Emails.Add(email);
        await Context.SaveChangesAsync();

        Hangfire.BackgroundJob.Enqueue<IEmailService>(x => x.ExecuteSendEmail(savedMail.Entity.Id.ToString()));
    }

    [AutomaticRetry(Attempts = 5, DelaysInSeconds = new int[] { 10, 30, 60, 120, 240 })]
    public async Task ExecuteSendEmail(string emailId)
    {
        try
        {
            var email = await Context.Emails.FindAsync(Guid.Parse(emailId));

            if (email != null)
            {
                if (email.IsSMTPEmail)
                {
                    await SendSMTPEmail(email);
                }
                else
                {
                    await SendProviderEmail(email);
                }
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            throw;
        }
    }

    private async Task SendSMTPEmail(Repository.Models.Email email)
    {
        using (MailMessage mail = new MailMessage())
        {
            mail.From = new MailAddress(email.From, email.FromName);
            mail.To.Add(String.Join(";", email.To));
            mail.Subject = email.Subject;
            mail.IsBodyHtml = true;
            mail.Body = email.HtmlBody;

            if (email.CC != null && email.CC.Count > 0)
            {
                mail.CC.Add(String.Join(";", email.CC));
            }

            if (email.BCC != null && email.BCC.Count > 0)
            {
                mail.Bcc.Add(String.Join(";", email.BCC));
            }

            if (email.ReplyTo != null && email.ReplyTo.Count > 0)
            {
                mail.ReplyToList.Add(String.Join(";", email.ReplyTo));
            }

            if (email.AttachmentFileNames != null)
            {
                foreach (string attachmentFilename in email.AttachmentFileNames)
                {
                    Attachment attachment = new Attachment(attachmentFilename, MediaTypeNames.Application.Octet);
                    ContentDisposition disposition = attachment.ContentDisposition;
                    disposition.CreationDate = System.IO.File.GetCreationTime(attachmentFilename);
                    disposition.ModificationDate = System.IO.File.GetLastWriteTime(attachmentFilename);
                    disposition.ReadDate = System.IO.File.GetLastAccessTime(attachmentFilename);
                    disposition.FileName = Path.GetFileName(attachmentFilename);
                    disposition.Size = new FileInfo(attachmentFilename).Length;
                    disposition.DispositionType = DispositionTypeNames.Attachment;
                    mail.Attachments.Add(attachment);
                }
            }

            using (var smtpClient = new SmtpClient())
            {
                NetworkCredential credential = new NetworkCredential(emailConfig.Value.Username, emailConfig.Value.Password);
                smtpClient.EnableSsl = emailConfig.Value.EnableSSL;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Port = emailConfig.Value.Port;
                smtpClient.Host = emailConfig.Value.Host;
                smtpClient.Credentials = credential;

                await smtpClient.SendMailAsync(mail);
            }
        }
    }

    private async Task SendProviderEmail(Repository.Models.Email email)
    {
        using (MailMessage mail = new MailMessage())
        {
            using (var httpClient = httpClientFactory.CreateClient())
            using (var smtpClient = new SmtpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + sendGridConfig.Value.SendGridApiKey);
                httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
                httpClient.BaseAddress =
                    new Uri(sendGridConfig.Value.SendGridGetTemplateURL + sendGridConfig.Value.SendGridTemplateId);

                var template = await httpClient.GetAsync(httpClient.BaseAddress.ToString());
                var contents = await template.Content.ReadAsStringAsync();

                if (template.StatusCode == HttpStatusCode.OK)
                {
                    var jsonTemplate = JsonSerializer.Serialize(contents);
                    if (email.KeyValuePairs != null)
                    {
                        foreach (var kv in email.KeyValuePairs)
                        {
                            if (jsonTemplate.Contains(kv.Key.ToString()))
                            {
                                jsonTemplate.Replace(kv.Key.ToString(), kv.Value.ToString());
                            }
                        }
                    }

                    mail.From = new MailAddress(email.From);
                    mail.To.Add(String.Join(";", email.To));
                    mail.ReplyToList.Add(String.Join(";", email.ReplyTo));
                    mail.CC.Add(String.Join(";", email.CC));
                    mail.Bcc.Add(String.Join(";", email.BCC));
                    mail.Body = jsonTemplate;
                    mail.Subject = email.Subject;
                    mail.IsBodyHtml = true;

                    NetworkCredential credential =
                        new NetworkCredential(emailConfig.Value.Username, emailConfig.Value.Password);
                    smtpClient.EnableSsl = emailConfig.Value.EnableSSL;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Port = emailConfig.Value.Port;
                    smtpClient.Host = emailConfig.Value.Host;
                    smtpClient.Credentials = credential;

                    await smtpClient.SendMailAsync(mail);
                }
            }
        }
    }
}