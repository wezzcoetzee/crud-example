using Crud.Domain.Enums;
using FluentValidation;

namespace Crud.Application.Assets.Commands.Update;

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

        RuleFor(x => x.Class)
            .Equal(AssetClass.Undefined).WithMessage("Class cannot be Undefined.")
            .NotEmpty().WithMessage("Class is required.");
    }
}