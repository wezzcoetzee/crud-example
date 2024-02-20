using Crud.Api.Infrastructure;
using Crud.Application.Assets.Commands;
using Crud.Application.Assets.Commands.Create;
using Crud.Application.Assets.Commands.Delete;
using Crud.Application.Assets.Commands.Update;
using Crud.Application.Assets.Queries;
using Crud.Application.Assets.Queries.Get;
using Crud.Application.Assets.Queries.GetAll;
using Crud.Domain.Dtos;
using Crud.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

    private static async Task<Guid> CreateAsset(ISender sender, [FromBody]CreateAssetDto request)
    {
        return await sender.Send(new CreateAssetCommand(request.Name, request.Ticker));
    }

    private static async Task<IResult> UpdateAsset(ISender sender, Guid id, UpdateAssetDto request)
    {
        if (id != request.Id) return Results.BadRequest();
        await sender.Send(new UpdateAssetCommand(request.Id, request.Name, request.Ticker));
        return Results.NoContent();
    }

    private static async Task<IResult> DeleteAsset(ISender sender, Guid id)
    {
        await sender.Send(new DeleteAssetCommand(id));
        return Results.NoContent();
    }
}