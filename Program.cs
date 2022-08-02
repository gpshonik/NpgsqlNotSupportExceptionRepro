using Microsoft.EntityFrameworkCore;
using NpgsqlNotSupportExceptionRepro;
using NpgsqlNotSupportExceptionRepro.Models;

var builder = Host.CreateDefaultBuilder(args)
    .UseSystemd()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<DemoDBContext>(options => options.UseNpgsql(hostContext.Configuration.GetConnectionString("PGConnectionString"), x =>
            x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
             .EnableRetryOnFailure())
             .UseSnakeCaseNamingConvention()
             .EnableSensitiveDataLogging(true));

        services.AddHostedService<Worker>();
    });

var host = builder.Build();

await host.RunAsync();
