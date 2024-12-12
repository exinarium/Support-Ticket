namespace SupportTicket.API.Domain.Repository.Models;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public DbSet<Email> Emails { get; set; }

    public DbSet<Account> Accounts { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<File> Files { get; set; }

    public DbSet<Ticket> Tickets { get; set; }

    public DbSet<TicketHistory> TicketHistory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}