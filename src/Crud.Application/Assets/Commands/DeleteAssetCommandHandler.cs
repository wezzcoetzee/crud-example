using Ardalis.GuardClauses;
using Crud.Application.Common.Interfaces;
using Crud.Domain.Events;
using FluentValidation;
using MediatR;

namespace Crud.Application.Assets.Commands;

public record DeleteAssetCommand(Guid Id) : IRequest;

public class DeleteAssetCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteAssetCommand>
{
    public async Task Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Assets
            .FindAsync([request.Id], cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        context.Assets.Remove(entity);

        entity.AddDomainEvent(new AssetDeletedEvent(entity));

        await context.SaveChangesAsync(cancellationToken);
    }
}

public class DeleteAssetCommandValidator : AbstractValidator<DeleteAssetCommand>
{
    public DeleteAssetCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}