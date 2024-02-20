using Crud.Application.Assets.Commands.Delete;
using FluentValidation.TestHelper;
using Xunit;

namespace Crud.UnitTests.Application.Assets.Commands.Delete;

public class DeleteAssetCommandValidatorTests
{
    private readonly DeleteAssetCommandValidator _validator = new();

    [Fact]
    public void ShouldHaveErrorWhenIdIsEmpty()
    {
        var command = new DeleteAssetCommand(Guid.Empty);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}