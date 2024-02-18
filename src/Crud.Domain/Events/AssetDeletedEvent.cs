using Crud.Domain.Common;
using Crud.Domain.Entities;

namespace Crud.Domain.Events;

public class AssetDeletedEvent(Asset asset) : BaseEvent
{
    public Asset Asset { get; } = asset;
}