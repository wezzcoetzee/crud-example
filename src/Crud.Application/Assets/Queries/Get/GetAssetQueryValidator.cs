using FluentValidation;

namespace Crud.Application.Assets.Queries.Get;

public class GetAssetQueryValidator : AbstractValidator<GetAssetQuery>
{
    public GetAssetQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}