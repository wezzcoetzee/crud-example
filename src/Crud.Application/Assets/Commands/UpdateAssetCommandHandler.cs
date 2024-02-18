using Ardalis.GuardClauses;
using Crud.Application.Common.Interfaces;
using Crud.Domain.Enums;
using Crud.Domain.Events;
using FluentValidation;
using MediatR;

namespace Crud.Application.Assets.Commands;

public record UpdateAssetCommand(Guid Id, string Name, string Ticker, AssetClass Class) : IRequest;

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

public class UpdateAssetCommandValidator : AbstractValidator<UpdateAssetCommand>
{
    public UpdateAssetCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.Ticker)
            .NotEmpty().WithMessage("Ticker is required.");

        // TODO Check is not Unknown
        RuleFor(x => x.Class)
            .IsInEnum()
            .NotEmpty().WithMessage("Class is required.");
    }
}