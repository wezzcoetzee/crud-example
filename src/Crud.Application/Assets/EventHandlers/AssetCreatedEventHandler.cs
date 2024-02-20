using Crud.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Crud.Application.Assets.EventHandlers;

public class AssetCreatedEventHandler(ILogger<AssetCreatedEventHandler> logger)
    : INotificationHandler<AssetCreatedEvent>
{
    public Task Handle(AssetCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Asset successfully created: {AssetId}", notification.Asset.Id);

        return Task.CompletedTask;
    }
}