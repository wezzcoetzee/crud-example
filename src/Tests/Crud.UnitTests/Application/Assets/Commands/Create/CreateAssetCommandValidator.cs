using Xunit;
using FluentValidation.TestHelper;
using Crud.Application.Assets.Commands.Create;

namespace Crud.UnitTests.Application.Assets.Commands.Create;

public class CreateAssetCommandValidatorTests
{
    private readonly CreateAssetCommandValidator _validator = new();

    [Fact]
    public void ShouldHaveErrorWhenNameIsEmpty()
    {
        var model = new CreateAssetCommand(string.Empty, "ValidTicker");
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void ShouldNotHaveErrorWhenNameIsSpecified()
    {
        var model = new CreateAssetCommand("ValidName", "ValidTicker");
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void ShouldHaveErrorWhenTickerIsEmpty()
    {
        var model = new CreateAssetCommand("ValidName", string.Empty);
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Ticker);
    }

    [Fact]
    public void ShouldNotHaveErrorWhenTickerIsSpecified()
    {
        var model = new CreateAssetCommand("ValidName", "ValidTicker");
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Ticker);
    }
}