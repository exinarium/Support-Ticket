using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SupportTicket.API.Domain.Repository.Models;

[Table("files")]
public class File
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [MaxLength(100)]
    [Required]
    [Column("fileName")]
    public string FileName { get; set; }

    [MaxLength(20)]
    [Required]
    [Column("contentType")]
    public string ContentType { get; set; }

    [MaxLength(255)]
    [Required]
    [Column("filePath")]
    public string FilePath { get; set; }

    [Required]
    [Column("fileSize")]
    public long FileSize { get; set; }

    [Required]
    [Column("uploadedDateTime")]
    public DateTime UploadDateTime { get; set; }

    [Column("uploadedBy")]
    public Guid? UploadedById { get; set; }

    [ForeignKey("UploadedById")]
    public User UploadedBy { get; set; }

    [Column("ticket")]
    public Guid? TicketId { get; set; }

    [ForeignKey("TicketId")]
    public Ticket? Ticket { get; set; }

    [Column("comment")]
    public Guid? CommentId { get; set; }

    [ForeignKey("CommentId")]
    public Comment? Comment { get; set; }
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