using System.Data.Common;
using Crud.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Testcontainers.PostgreSql;
using Xunit;

namespace Crud.IntegrationTests;

public class CrudFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithName("crud")
        .WithImage("postgres:latest")
        .WithPortBinding(54321, 5432)
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(
            services =>
            {
                services.AddTransient(provider =>
                {
                    var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
                    const string categoryName = "crud-integration";
                    return loggerFactory.CreateLogger(categoryName);
                });
            });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        await using var dbContext = GetDbContext();
        await dbContext.Database.MigrateAsync();
    }

    public (IServiceScope Scope, ApplicationDbContext Db) GetDb()
    {
        var scope = Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
        return (scope, db);
    }

    async Task IAsyncLifetime.DisposeAsync() => await _dbContainer.StopAsync();

    private ApplicationDbContext GetDbContext()
    {
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseNpgsql(_dbContainer.GetConnectionString());
        return new ApplicationDbContext(builder.Options);
    }
}