using Crud.Api.Infrastructure;
using Crud.Application.Assets.Queries;
using Crud.Domain.Entities;
using MediatR;

namespace Crud.Api.Endpoints;

public class AssetEndpoints : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetAssets);
    }

    private static async Task<List<Asset>> GetAssets(ISender sender)
    {
        return await sender.Send(new GetAssetsQuery());
    }
}
