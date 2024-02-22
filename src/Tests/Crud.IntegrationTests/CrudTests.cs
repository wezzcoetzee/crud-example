using System.Net;
using AutoFixture;
using Crud.Domain.Entities;
using Xunit;
using Xunit.Abstractions;

namespace Crud.IntegrationTests;

public class CrudTests : IAsyncLifetime
{
    protected readonly CrudFactory Factory;
    protected readonly ITestOutputHelper TestOutputHelper;
    
    protected static IFixture AutoFixture
    {
        get
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture;
        }
    }

    protected CrudTests(CrudFactory factory, 
        ITestOutputHelper testOutputHelper)
    {
        Factory = factory;
        TestOutputHelper = testOutputHelper;
    }

    protected async Task<Asset> GenerateAssetAsync(
        Guid? id = null,
        string name = "Bitcoin",
        string ticker = "BTC")
    {
        var asset = new Asset { Id = id ?? Guid.NewGuid(), Name = name, Ticker = ticker };

        var (scope, dbContext) = Factory.GetDb();
        using (scope)
        {
            dbContext.Assets.Add(asset);
            await dbContext.SaveChangesAsync();
        }

        return asset;
    }

    public async Task InitializeAsync()
    {
        await WaitServiceReadyAsync();
    }

    public async Task DisposeAsync()
    {
    }
    
    private async Task WaitServiceReadyAsync()
    {
        using var client = Factory.CreateClient();

        var healthy = false;
        var ready = false;
        const int maxRetries = 30;
        var retryCount = 0;
        while ((!healthy || !ready) && retryCount < maxRetries)
        {
            await Task.Delay(1000);

            var healthResult = await client.GetAsync("/healthz");
            var readyResult = await client.GetAsync("/readiness");
            healthy = healthResult.StatusCode == HttpStatusCode.OK;
            ready = readyResult.StatusCode == HttpStatusCode.OK;

            retryCount++;
            if ((!healthy || !ready) && retryCount == maxRetries)
            {
                TestOutputHelper.WriteLine(await healthResult.Content.ReadAsStringAsync());
                TestOutputHelper.WriteLine(await readyResult.Content.ReadAsStringAsync());

                throw new Exception($"Service not ready after {maxRetries} seconds.");
            }
        }
    }
}