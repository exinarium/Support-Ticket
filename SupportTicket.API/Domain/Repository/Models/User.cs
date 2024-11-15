using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SupportTicket.API.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SupportTicket.API.Domain.Repository.Models;

[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("email")]
    public string Email { get; set; }

    [Required]
    [Column("password")]
    public string Password { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("firstName")]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("lastName")]
    public string LastName { get; set; }

    [Column("isEmailConfirmed")]
    public bool IsEmailConfirmed { get; set; }

    [Column("isLockoutEnabled")]
    public bool IsLockoutEnabled { get; set; }

    [Column("isLocked")]
    public bool IsLocked { get; set; }

    [Column("accessFailedCount")]
    public int AccessFailedCount { get; set; }

    [Required]
    [Column("createDateTime")]
    public DateTime CreatedDateTime { get; set; }

    [Column("updateDateTime")]
    public DateTime? UpdatedDateTime { get; set; }

    // Foreign keys
    [Required]
    [Column("createBy")]
    public Guid CreatedById { get; set; }

    [Column("updateBy")]
    public Guid? UpdatedById { get; set; }

    // Navigation properties
    [ForeignKey("CreatedById")]
    public virtual User CreatedByUser { get; set; }

    [ForeignKey("UpdatedById")]
    public virtual User? UpdatedByUser { get; set; }

    [Column("account")]
    public Guid AccountId { get; set; }

    [ForeignKey("AccountId")]
    public virtual Account Account { get; set; }

    [Column("isActive")]
    public bool IsActive { get; set; } = true;

    // Inverse navigation properties
    public virtual ICollection<User> CreatedUsers { get; set; }
    public virtual ICollection<User> UpdatedUsers { get; set; }
    public virtual ICollection<Ticket> CreatedTickets { get; set; }
    public virtual ICollection<Ticket> UpdatedTickets { get; set; }
    public virtual ICollection<Ticket> AssignedTickets { get; set; }
    public virtual ICollection<File> Files { get; set; }

    public virtual ICollection<Comment> UpdatedComments { get; set; }

    public virtual ICollection<Comment> CreatedComments { get; set; }

    public User()
    {
        CreatedUsers = new HashSet<User>();
        UpdatedUsers = new HashSet<User>();
        CreatedTickets = new HashSet<Ticket>();
        AssignedTickets = new HashSet<Ticket>();
        UpdatedTickets = new HashSet<Ticket>();
        Files = new HashSet<File>();
        UpdatedComments = new HashSet<Comment>();
        CreatedComments = new HashSet<Comment>();
    }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.Email)
            .HasDatabaseName("IX_Users_Email")
            .IsUnique();

        builder.HasOne(c => c.CreatedByUser)
            .WithMany(o => o.CreatedUsers)
            .HasForeignKey(o => o.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u => u.UpdatedByUser)
            .WithMany(u => u.UpdatedUsers)
            .HasForeignKey(u => u.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        builder.HasData(new User()
        {
            Id = Guid.Parse("863e485c-196d-41e0-ad03-90e94c3890af"),
            FirstName = "admin",
            LastName = "Creativ360",
            Email = "admin@creativ360.com",
            AccountId = Guid.Parse("10dce69c-ac67-40d9-927b-86fe23206a90"),
            Password = PasswordHasher.HashPassword("P@ssw0rd"),
            CreatedById = Guid.Parse("863e485c-196d-41e0-ad03-90e94c3890af"),
            IsEmailConfirmed = true,
            IsLockoutEnabled = false,
            IsLocked = false,
            IsActive = true,
            CreatedDateTime = DateTime.UtcNow
        });
    }
}