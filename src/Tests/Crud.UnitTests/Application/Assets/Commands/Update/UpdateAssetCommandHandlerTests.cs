using Xunit;
using FluentAssertions;
using NSubstitute;
using Crud.Application.Common.Interfaces;
using Crud.Application.Assets.Commands.Update;
using Crud.Domain.Entities;

namespace Crud.UnitTests.Application.Assets.Commands.Update;

public class UpdateAssetCommandHandlerTests
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly UpdateAssetCommandHandler _handler;

    public UpdateAssetCommandHandlerTests()
    {
        _context = Substitute.For<IApplicationDbContext>();
        _dateTimeProvider = Substitute.For<IDateTimeProvider>();
        _handler = new UpdateAssetCommandHandler(_context, _dateTimeProvider);
    }

    [Fact]
    public async Task Handle_ShouldUpdateAssetAndSaveChanges()
    {
        // Arrange
        var asset = new Asset { Id = Guid.NewGuid(), Name = "Old Name", Ticker = "Old Ticker" };
        var command = new UpdateAssetCommand(asset.Id, "New Name", "New Ticker");
        _context.Assets.FindAsync(Arg.Any<object?[]?>(), Arg.Any<CancellationToken>()).Returns(asset);
        _dateTimeProvider.UtcNow.Returns(DateTime.UtcNow);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        asset.Name.Should().Be(command.Name);
        asset.Ticker.Should().Be(command.Ticker);
        await _context.Received().SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}