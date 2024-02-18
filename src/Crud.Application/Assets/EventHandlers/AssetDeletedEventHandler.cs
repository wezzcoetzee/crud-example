using Crud.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Crud.Application.Assets.EventHandlers;

public class AssetDeletedEventHandler(ILogger<AssetDeletedEventHandler> logger)
    : INotificationHandler<AssetDeletedEvent>
{
    public Task Handle(AssetDeletedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}