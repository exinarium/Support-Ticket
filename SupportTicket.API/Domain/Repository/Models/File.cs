namespace SupportTicket.API.Domain.Repository.Models;

[Table("files")]
public class File
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(100)]
    [Required]
    [Column("fileName")]
    public string FileName { get; set; } = string.Empty;

    [MaxLength(20)]
    [Required]
    [Column("contentType")]
    public string ContentType { get; set; } = string.Empty;

    [MaxLength(255)]
    [Required]
    [Column("filePath")]
    public string FilePath { get; set; } = string.Empty;

    [Required]
    [Column("fileSize")]
    public long FileSize { get; set; }

    [Required]
    [Column("uploadedDateTime")]
    public DateTime UploadDateTime { get; set; } = DateTime.UtcNow;

    [Column("uploadedBy")]
    public Guid? UploadedById { get; set; }

    [ForeignKey("UploadedById")]
    public virtual User? UploadedBy { get; set; }

    [Column("ticket")]
    public Guid? TicketId { get; set; }

    [ForeignKey("TicketId")]
    public virtual Ticket? Ticket { get; set; }

    [Column("comment")]
    public Guid? CommentId { get; set; }

    [ForeignKey("CommentId")]
    public virtual Comment? Comment { get; set; }
}

public class FileConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder.HasOne(h => h.Ticket)
            .WithMany(t => t.Attachments)
            .HasForeignKey(h => h.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(h => h.Comment)
            .WithMany(t => t.Attachments)
            .HasForeignKey(h => h.CommentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(h => h.UploadedBy)
            .WithMany(t => t.Files)
            .HasForeignKey(h => h.UploadedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(h => h.TicketId)
            .HasDatabaseName("IX_File_TicketId");

        builder.HasIndex(h => h.CommentId)
            .HasDatabaseName("IX_File_CommentId");
    }
}