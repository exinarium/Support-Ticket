using SupportTicket.API.Domain.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace SupportTicket.API.Domain.Repository.Models;

public class DataContext(IOptions<DatabaseConfig> databaseConfig) : DbContext
{
    private DatabaseConfig _databaseConfig => databaseConfig.Value;

    public DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionString:
            $"Server={_databaseConfig.Host};Port={_databaseConfig.Port};User Id={_databaseConfig.Username};Password={_databaseConfig.Password};Database={_databaseConfig.DatabaseName};");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}