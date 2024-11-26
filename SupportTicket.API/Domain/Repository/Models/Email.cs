using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupportTicket.SDK.Enums;

namespace SupportTicket.API.Domain.Repository.Models;

[Table("emails")]
public class Email
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("to")]
    [Required]
    public List<string> To { get; set; }

    [Column("cc")]
    public List<string> CC { get; set; }

    [Column("bcc")]
    public List<string> BCC { get; set; }

    [Column("replyTo")]
    public List<string> ReplyTo { get; set; }

    [Column("htmlBody")]
    [Required]
    public string HtmlBody { get; set; }

    [Column("emailType")]
    [Required]
    public EmailType EmailType { get; set; }

    [Column("fromName")]
    [Required]
    public string FromName { get; set; }

    [Column("subject")]
    [Required]
    public string Subject { get; set; }

    [Column("from")]
    [Required]
    public string From { get; set; }

    [Column("attachmentFileNames")]
    public List<string> AttachmentFileNames { get; set; }

    [Column("isHtml")]
    public bool IsHtml { get; set; }

    [Column("isSMTPEmail")]
    [Required]
    public bool IsSMTPEmail { get; set; }

    [Column("keyValuePairs")]
    public Dictionary<string, string> KeyValuePairs { get; set; }

    [Required]
    [Column("createDateTime")]
    public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;

    [Required]
    [Column("createdBy")]
    public Guid CreatedById { get; set; } = Guid.Empty;

    [ForeignKey("CreatedById")]
    public User CreatedBy { get; set; } = new();
}

public class EmailConfiguration : IEntityTypeConfiguration<Email>
{
    public void Configure(EntityTypeBuilder<Email> builder)
    {
        builder.HasOne(c => c.CreatedBy)
            .WithMany(o => o.CreatedEmails)
            .HasForeignKey(o => o.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}