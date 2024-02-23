using AutoFixture;
using Xunit;
using FluentAssertions;
using NSubstitute;
using Crud.Application.Common.Interfaces;
using Crud.Application.Assets.Commands.Update;
using Crud.Domain.Entities;
using Crud.Infrastructure.Data;
using Crud.UnitTests.Helpers;

namespace Crud.UnitTests.Application.Assets.Commands.Update;

public class UpdateAssetCommandHandlerTests : DatabaseHelper
{
    private readonly IFixture _fixture;
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly UpdateAssetCommandHandler _handler;

    public UpdateAssetCommandHandlerTests()
    {
        _fixture = new Fixture();
        _context = new ApplicationDbContext(GetInMemoryDb);
        _dateTimeProvider = Substitute.For<IDateTimeProvider>();
        _handler = new UpdateAssetCommandHandler(_context, _dateTimeProvider);
    }
    
    [Fact]
    public async Task Handle_ShouldUpdateAssetAndSaveChanges()
    {
        // Arrange
        var asset = _fixture.Create<Asset>();

        await _context.Assets.AddAsync(asset);
        await _context.SaveChangesAsync(default);
        
        _dateTimeProvider.UtcNow.Returns(DateTime.UtcNow);
        
        var command = new UpdateAssetCommand(asset.Id, "Ethereum", "ETH");

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var updatedAsset = _context.Assets.FirstOrDefault(f => f.Id == asset.Id);
        updatedAsset.Should().NotBeNull();
        updatedAsset!.Name.Should().Be(command.Name);
        updatedAsset.Ticker.Should().Be(command.Ticker);
    }

    [Fact]
    public async Task Handle_Should_CallSaveChangesAsync()
    {
        // Arrange
        var context = Substitute.For<IApplicationDbContext>();
        var handler = new UpdateAssetCommandHandler(context, _dateTimeProvider);
        
        var asset = _fixture.Create<Asset>();
        
        var command = new UpdateAssetCommand(asset.Id, "Bitcoin", "BTC");
        
        context.Assets.FindAsync(Arg.Any<object?[]?>(), Arg.Any<CancellationToken>()).Returns(asset);
        _dateTimeProvider.UtcNow.Returns(DateTime.UtcNow);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        asset.Name.Should().Be(command.Name);
        asset.Ticker.Should().Be(command.Ticker);
        await context.Received().SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}