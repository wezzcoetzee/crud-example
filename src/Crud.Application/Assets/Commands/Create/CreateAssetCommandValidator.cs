using Crud.Domain.Enums;
using FluentValidation;

namespace Crud.Application.Assets.Commands.Create;

public class CreateAssetCommandValidator : AbstractValidator<CreateAssetCommand>
{
    public CreateAssetCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.Ticker)
            .NotEmpty().WithMessage("Ticker is required.");
        
        RuleFor(x => x.Class)
            .Equal(AssetClass.Undefined).WithMessage("Class cannot be Undefined.")
            .NotEmpty().WithMessage("Class is required.");
    }
}