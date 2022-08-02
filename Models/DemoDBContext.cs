using Microsoft.EntityFrameworkCore;

namespace NpgsqlNotSupportExceptionRepro.Models;

public class DemoDBContext : DbContext
{
    public DemoDBContext(DbContextOptions<DemoDBContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("demo")
            .HasPostgresExtension("extensions", "citext");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<string>()
            .HaveColumnType("citext");
    }
}
