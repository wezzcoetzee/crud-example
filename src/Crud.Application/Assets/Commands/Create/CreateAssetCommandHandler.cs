using Crud.Application.Common.Interfaces;
using Crud.Domain.Entities;
using Crud.Domain.Events;
using MediatR;

namespace Crud.Application.Assets.Commands.Create;

public class CreateAssetCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateAssetCommand, Guid>
{
    public async Task<Guid> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
    {
        var entity = new Asset
        {
            Class = request.Class,
            Name = request.Name,
            Ticker = request.Ticker
        };
        
        entity.AddDomainEvent(new AssetCreatedEvent(entity));

        context.Assets.Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}