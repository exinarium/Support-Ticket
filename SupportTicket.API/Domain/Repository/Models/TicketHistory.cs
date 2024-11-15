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
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("field")]
    public string Field { get; set; }

    [Required]
    [Column("oldValue")]
    public string OldValue { get; set; }

    [Required]
    [Column("newValue")]
    public string NewValue { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("description")]
    public string Description { get; set; }

    [Required]
    [Column("changedDateTime")]
    public DateTime ChangedDateTime { get; set; }

    [Required]
    [Column("ticket")]
    public Guid TicketId { get; set; }

    [ForeignKey("TicketId")]
    public Ticket Ticket { get; set; }

    [Required]
    [Column("ChangedBy")]
    public Guid ChangedById { get; set; }

    [ForeignKey("ChangedById")]
    public User ChangedBy { get; set; }
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