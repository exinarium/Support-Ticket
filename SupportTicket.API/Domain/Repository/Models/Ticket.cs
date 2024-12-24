namespace SupportTicket.API.Domain.Repository.Models;

[Table("tickets")]
public class Ticket
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    [Column("subject")]
    public string Subject { get; set; } = string.Empty;

    [Required]
    [Column("message")]
    [MaxLength(1000)]
    public string Message { get; set; } = string.Empty;

    [Required]
    [Column("status")]
    public TicketStatus Status { get; set; } = TicketStatus.Open;

    [Required]
    [Column("priority")]
    public TicketPriority Priority { get; set; } = TicketPriority.Medium;

    [Column("tags", TypeName = "text[]")]
    public string[] Tags { get; set; } = [];

    [Required]
    [Column("createDateTime")]
    public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;

    [Column("updateDateTime")]
    public DateTime? UpdatedDateTime { get; set; }

    [Required]
    [Column("createdBy")]
    public Guid CreatedById { get; set; } = Guid.Empty;

    [ForeignKey("CreatedById")]
    public virtual User CreatedBy { get; set; } = new();

    [Column("updatedBy")]
    public Guid? UpdatedById { get; set; }

    [ForeignKey("UpdatedById")]
    public virtual User? UpdatedBy { get; set; }

    [Column("assignedTo")]
    public Guid? AssignedToId { get; set; }

    [ForeignKey("AssignedToId")]
    public virtual User? AssignedTo { get; set; }

    public virtual ICollection<TicketHistory> History { get; set; } = [];

    public virtual ICollection<Comment> Comments { get; set; } = [];

    public virtual ICollection<File> Attachments { get; set; } = [];
}

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasOne(c => c.CreatedBy)
            .WithMany(o => o.CreatedTickets)
            .HasForeignKey(o => o.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.UpdatedBy)
            .WithMany(o => o.UpdatedTickets)
            .HasForeignKey(o => o.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.AssignedTo)
            .WithMany(o => o.AssignedTickets)
            .HasForeignKey(o => o.AssignedToId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(u => u.Status)
            .HasColumnType("varchar(20)")
            .HasConversion(
                ticketStatus => ticketStatus.ToString(),
                ticketStatus => (TicketStatus)Enum.Parse(typeof(TicketStatus), ticketStatus));

        builder.Property(u => u.Priority)
            .HasColumnType("varchar(20)")
            .HasConversion(
                ticketPriority => ticketPriority.ToString(),
                ticketPriority => (TicketPriority)Enum.Parse(typeof(TicketPriority), ticketPriority));

        builder.HasIndex(p => p.Status)
            .HasDatabaseName("IX_Ticket_Status");

        builder.HasIndex(p => p.AssignedToId)
            .HasDatabaseName("IX_Ticket_AssignedTo");

        builder.Property(p => p.Tags)
            .HasColumnType("text[]")
            .IsRequired(false);

        builder.HasIndex(p => p.Tags)
            .HasMethod("GIN")
            .HasDatabaseName("IX_Ticket_Tags");
    }
}