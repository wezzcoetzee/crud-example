using Crud.Application.Assets.Commands.Delete;
using Crud.Application.Common.Interfaces;
using Crud.Domain.Entities;
using NSubstitute;
using Xunit;

namespace Crud.UnitTests.Application.Assets.Commands.Delete;

public class DeleteAssetCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldRemoveAssetAndSaveChanges()
    {
        // Arrange
        var dbContext = Substitute.For<IApplicationDbContext>();
        var asset = new Asset { Id = Guid.NewGuid() };
        dbContext.Assets.FindAsync(Arg.Any<object?[]?>(), Arg.Any<CancellationToken>()).Returns(asset);

        var command = new DeleteAssetCommand(asset.Id);
        var handler = new DeleteAssetCommandHandler(dbContext);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        dbContext.Assets.Received(1).Remove(asset);
        await dbContext.Received(1).SaveChangesAsync(CancellationToken.None);
    }
}