using SupportTicket.API.Domain.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace SupportTicket.API.Domain.Repository.Models;

public class DataContext(IOptions<DatabaseConfig> databaseConfig) : DbContext
{
    private DatabaseConfig DatabaseConfig => databaseConfig.Value;

    public DbSet<User> Users { get; set; }

    public DbSet<Email> Emails { get; set; }

    public DbSet<Account> Accounts { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<File> Files { get; set; }

    public DbSet<Ticket> Tickets { get; set; }

    public DbSet<TicketHistory> TicketHistory { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionString:
            $"Server={DatabaseConfig.Host};Port={DatabaseConfig.Port};User Id={DatabaseConfig.Username};Password={DatabaseConfig.Password};Database={DatabaseConfig.DatabaseName};");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}