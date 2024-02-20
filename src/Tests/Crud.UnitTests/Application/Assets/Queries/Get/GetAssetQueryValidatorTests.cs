using Crud.Application.Assets.Queries.Get;
using FluentValidation.TestHelper;
using Xunit;

namespace Crud.UnitTests.Application.Assets.Queries.Get;

public class GetAssetQueryValidatorTests
{
    private readonly GetAssetQueryValidator _validator = new();

    [Fact]
    public void ShouldHaveErrorWhenIdIsEmpty()
    {
        var model = new GetAssetQuery(Guid.Empty);

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void ShouldNotHaveErrorWhenIdIsSpecified()
    {
        var model = new GetAssetQuery(Guid.NewGuid());

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }
}