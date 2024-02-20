using Crud.Api.Infrastructure;
using Crud.Application.Assets.Commands;
using Crud.Application.Assets.Commands.Create;
using Crud.Application.Assets.Commands.Delete;
using Crud.Application.Assets.Commands.Update;
using Crud.Application.Assets.Queries;
using Crud.Application.Assets.Queries.Get;
using Crud.Application.Assets.Queries.GetAll;
using Crud.Domain.Entities;
using MediatR;

namespace Crud.Api.Endpoints;

public class AssetEndpoints : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetAssets)
            .MapGet(GetAsset, "{id}")
            .MapPost(CreateAsset)
            .MapPut(UpdateAsset, "{id}")
            .MapDelete(DeleteAsset, "{id}");
    }

    private static async Task<List<Asset>> GetAssets(ISender sender)
    {
        return await sender.Send(new GetAssetsQuery());
    }

    private static async Task<Asset?> GetAsset(ISender sender, Guid id)
    {
        return await sender.Send(new GetAssetQuery(id));
    }

    private static async Task<Guid> CreateAsset(ISender sender, CreateAssetCommand command)
    {
        return await sender.Send(command);
    }

    private static async Task<IResult> UpdateAsset(ISender sender, Guid id, UpdateAssetCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    private static async Task<IResult> DeleteAsset(ISender sender, Guid id)
    {
        await sender.Send(new DeleteAssetCommand(id));
        return Results.NoContent();
    }
}