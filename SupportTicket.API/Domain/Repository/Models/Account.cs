namespace SupportTicket.API.Domain.Repository.Models;

[Table("account")]
public class Account
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(50)]
    [Required]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(30)]
    [Required]
    [Column("contactName")]
    public string ContactName { get; set; } = string.Empty;

    [MaxLength(15)]
    [Required]
    [Column("contactTelephone")]
    public string ContactTelephone { get; set; } = string.Empty;

    [MaxLength(30)]
    [Required]
    [Column("contactEmail")]
    public string ContactEmail { get; set; } = string.Empty;

    [Column("isActive")]
    public bool IsActive { get; set; } = true;

    [Required]
    [Column("createDateTime")]
    public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;

    [Column("updateDateTime")]
    public DateTime? UpdatedDateTime { get; set; }

    public virtual ICollection<User> Users { get; set; } = [];
}

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasMany(c => c.Users)
            .WithOne(o => o.Account)
            .HasForeignKey(o => o.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(new Account()
        {
            Id = Guid.Parse("10dce69c-ac67-40d9-927b-86fe23206a90"),
            Name = "DefaultAccount",
            ContactName = "Creativ360 Contact",
            ContactEmail = "admin@creativ360.com",
            ContactTelephone = "+27741897705",
            IsActive = true,
            CreatedDateTime = DateTime.UtcNow
        });
    }
}