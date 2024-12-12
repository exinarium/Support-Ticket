namespace SupportTicket.API.Domain.Repository.Models;

[Table("comments")]
public class Comment
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(1000)]
    [Column("content")]
    public string Content { get; set; } = string.Empty;

    [Column("isInternal")]
    public bool IsInternal { get; set; }

    [Required]
    [Column("createdDateTime")]
    public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;

    [Column("updatedDateTime")]
    public DateTime? UpdatedDateTime { get; set; }

    [Required]
    [Column("ticket")]
    public Guid TicketId { get; set; } = Guid.Empty;

    [ForeignKey("TicketId")]
    public virtual Ticket Ticket { get; set; } = new();

    [Required]
    [Column("createdBy")]
    public Guid CreatedById { get; set; } = Guid.Empty;

    [ForeignKey("CreatedById")]
    public virtual User CreatedBy { get; set; } = new();

    [Column("updatedBy")]
    public Guid? UpdatedById { get; set; }

    [ForeignKey("UpdatedById")]
    public virtual User? UpdatedBy { get; set; }

    [Column("parentComment")]
    public Guid? ParentCommentId { get; set; }

    [ForeignKey("ParentCommentId")]
    public virtual Comment? ParentComment { get; set; }

    public virtual ICollection<Comment> Replies { get; set; } = [];

    public virtual ICollection<File> Attachments { get; set; } = [];
}

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasOne(h => h.Ticket)
            .WithMany(t => t.Comments)
            .HasForeignKey(h => h.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(h => h.CreatedBy)
            .WithMany(t => t.CreatedComments)
            .HasForeignKey(h => h.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(h => h.UpdatedBy)
            .WithMany(t => t.UpdatedComments)
            .HasForeignKey(h => h.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(h => h.Replies)
            .WithOne(t => t.ParentComment)
            .HasForeignKey(h => h.ParentCommentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(h => h.TicketId)
            .HasDatabaseName("IX_Comment_TicketId");
    }
}