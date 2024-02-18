using Crud.Application.Common.Interfaces;
using Crud.Domain.Entities;
using Crud.Domain.Enums;
using Crud.Domain.Events;
using FluentValidation;
using MediatR;

namespace Crud.Application.Assets.Commands;

public record CreateAssetCommand(string Name, string Ticker, AssetClass Class) : IRequest<Guid>;

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

public class CreateAssetCommandValidator : AbstractValidator<CreateAssetCommand>
{
    public CreateAssetCommandValidator()
    {
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