using Crud.Application.Assets.Commands.Update;
using FluentValidation.TestHelper;
using Xunit;

namespace Crud.UnitTests.Application.Assets.Commands.Update;

public class UpdateAssetCommandValidatorTests
{
    private readonly UpdateAssetCommandValidator _validator = new();

    [Fact]
    public void ShouldHaveErrorWhenIdIsEmpty()
    {
        var model = new UpdateAssetCommand(Guid.Empty, "Name", "Ticker");
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void ShouldHaveErrorWhenNameIsEmpty()
    {
        var model = new UpdateAssetCommand(Guid.NewGuid(), string.Empty, "Ticker");
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void ShouldHaveErrorWhenTickerIsEmpty()
    {
        var model = new UpdateAssetCommand(Guid.NewGuid(), "Name", string.Empty);
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Ticker);
    }
}