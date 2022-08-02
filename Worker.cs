using Microsoft.EntityFrameworkCore;
using NpgsqlNotSupportExceptionRepro.Models;

namespace NpgsqlNotSupportExceptionRepro;
public class Worker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public Worker(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<DemoDBContext>();

        // Works with Npgsql.EntityFrameworkCore.PostgreSQL 6.0.4
        // Throws System.NotSupportedException: The field 'name' has type 'extensions.citext', which is currently unknown to Npgsql supported exception with 6.0.5
        // Any query will do.

        var customers = await db.Customers.Where(c => c.Name == "Test Customer").ToListAsync();

        Console.WriteLine($"Found {customers.Count} customer(s)");
    }
}
