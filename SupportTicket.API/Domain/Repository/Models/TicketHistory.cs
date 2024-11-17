using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SupportTicket.API.Domain.Repository.Models;

[Table("ticketHistory")]
public class TicketHistory
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    [Column("field")]
    public string Field { get; set; } = string.Empty;

    [Required]
    [Column("oldValue")]
    [MaxLength(2000)]
    public string OldValue { get; set; } = string.Empty;

    [Required]
    [Column("newValue")]
    [MaxLength(2000)]
    public string NewValue { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Column("changedDateTime")]
    public DateTime ChangedDateTime { get; set; } = DateTime.UtcNow;

    [Required]
    [Column("ticket")]
    public Guid TicketId { get; set; } = Guid.Empty;

    [ForeignKey("TicketId")]
    public Ticket Ticket { get; set; } = new();

    [Required]
    [Column("ChangedBy")]
    public Guid ChangedById { get; set; } = Guid.Empty;

    [ForeignKey("ChangedById")]
    public User ChangedBy { get; set; } = new();
}

public class TicketHistoryConfiguration : IEntityTypeConfiguration<TicketHistory>
{
    public void Configure(EntityTypeBuilder<TicketHistory> builder)
    {
        builder.HasOne(h => h.Ticket)
            .WithMany(t => t.History)
            .HasForeignKey(h => h.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(h => h.ChangedBy)
            .WithMany()
            .HasForeignKey(h => h.ChangedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(h => h.TicketId)
            .HasDatabaseName("IX_TicketHistory_TicketId");

        builder.HasIndex(h => h.ChangedDateTime)
            .HasDatabaseName("IX_TicketHistory_ChangedDateTime");
    }
}