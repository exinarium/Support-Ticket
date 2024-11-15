using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using SupportTicket.SDK.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SupportTicket.API.Domain.Repository.Models;

[Table("tickets")]
public class Ticket
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("subject")]
    public string Subject { get; set; }

    [Required]
    [Column("message")]
    public string Message { get; set; }

    [Required]
    [Column("status")]
    public TicketStatus Status { get; set; }

    [Required]
    [Column("priority")]
    public TicketPriority Priority { get; set; }

    [Column("tags", TypeName = "text[]")]
    public string[] Tags { get; set; }

    [Required]
    [Column("createDateTime")]
    public DateTime CreatedDateTime { get; set; }

    [Column("updateDateTime")]
    public DateTime? UpdatedDateTime { get; set; }

    [Required]
    [Column("createdBy")]
    public Guid CreatedById { get; set; }

    [ForeignKey("CreatedById")]
    public User CreatedBy { get; set; }

    [Required]
    [Column("updatedBy")]
    public Guid UpdatedById { get; set; }

    [ForeignKey("UpdatedById")]
    public User UpdatedBy { get; set; }

    [Required]
    [Column("assignedTo")]
    public Guid AssignedToId { get; set; }

    [ForeignKey("AssignedToId")]
    public User AssignedTo { get; set; }

    public ICollection<TicketHistory> History { get; set; }

    public ICollection<Comment> Comments { get; set; }

    public ICollection<File> Attachments { get; set; }

    public Ticket()
    {
        History = new HashSet<TicketHistory>();
        Comments = new HashSet<Comment>();
        Attachments = new HashSet<File>();
    }
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