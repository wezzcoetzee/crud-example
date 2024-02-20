using Ardalis.GuardClauses;
using Crud.Application.Common.Interfaces;
using Crud.Domain.Events;
using MediatR;

namespace Crud.Application.Assets.Commands.Update;

public class UpdateAssetCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateAssetCommand>
{
    public async Task Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Assets
            .FindAsync([request.Id], cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.AddDomainEvent(new AssetUpdatedEvent(entity));

        entity.Name = request.Name;
        entity.Ticker = request.Ticker;
        entity.Class = request.Class;

        await context.SaveChangesAsync(cancellationToken);
    }
}