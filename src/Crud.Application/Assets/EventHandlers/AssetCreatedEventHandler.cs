using Crud.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Crud.Application.Assets.EventHandlers;

public class AssetCreatedEventHandler(ILogger<AssetCreatedEventHandler> logger)
    : INotificationHandler<AssetCreatedEvent>
{
    public Task Handle(AssetCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}