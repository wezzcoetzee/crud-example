using Crud.Application.Assets.Commands.Delete;
using Crud.Application.Common.Interfaces;
using Crud.Domain.Entities;
using NSubstitute;
using Xunit;

namespace Crud.UnitTests.Application.Assets.Commands.Delete;

public class DeleteAssetCommandHandlerTests
{
    private readonly IApplicationDbContext _context;
    private readonly DeleteAssetCommandHandler _handler;

    public DeleteAssetCommandHandlerTests()
    {
        _context = Substitute.For<IApplicationDbContext>();
        _handler = new DeleteAssetCommandHandler(_context);
    }
    
    [Fact]
    public async Task Handle_ShouldRemoveAssetAndSaveChanges()
    {
        // Arrange
        var asset = new Asset { Id = Guid.NewGuid() };
        _context.Assets.FindAsync(Arg.Any<object?[]?>(), Arg.Any<CancellationToken>()).Returns(asset);

        var command = new DeleteAssetCommand(asset.Id);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _context.Assets.Received(1).Remove(asset);
        await _context.Received(1).SaveChangesAsync(CancellationToken.None);
    }
}