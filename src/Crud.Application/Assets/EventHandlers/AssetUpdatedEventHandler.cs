using Crud.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Crud.Application.Assets.EventHandlers;

public class AssetUpdatedEventHandler(ILogger<AssetUpdatedEventHandler> logger)
    : INotificationHandler<AssetUpdatedEvent>
{
    public Task Handle(AssetUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}