using Crud.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Crud.Application.Assets.EventHandlers;

public class AssetUpdatedEventHandler(ILogger<AssetUpdatedEventHandler> logger)
    : INotificationHandler<AssetUpdatedEvent>
{
    public Task Handle(AssetUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Asset successfully updated: {AssetId}", notification.Asset.Id);

        return Task.CompletedTask;
    }
}