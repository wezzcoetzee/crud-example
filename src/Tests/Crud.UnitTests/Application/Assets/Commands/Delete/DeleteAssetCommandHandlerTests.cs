using AutoFixture;
using Crud.Application.Assets.Commands.Delete;
using Crud.Application.Common.Interfaces;
using Crud.Domain.Entities;
using Crud.Infrastructure.Data;
using Crud.UnitTests.Helpers;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Crud.UnitTests.Application.Assets.Commands.Delete;

public class DeleteAssetCommandHandlerTests : DatabaseHelper
{
    private readonly IFixture _fixture;
    private readonly IApplicationDbContext _context;
    private readonly DeleteAssetCommandHandler _handler;

    public DeleteAssetCommandHandlerTests()
    {
        _fixture = new Fixture();
        _context = new ApplicationDbContext(GetInMemoryDb);
        _handler = new DeleteAssetCommandHandler(_context);
    }
    
    [Fact]
    public async Task Handle_ShouldRemoveAssetAndSaveChanges()
    {
        // Arrange
        var asset = _fixture.Create<Asset>();

        await _context.Assets.AddAsync(asset);
        await _context.SaveChangesAsync(default);

        var command = new DeleteAssetCommand(asset.Id);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _context.Assets.FirstOrDefault(f => f.Id == asset.Id).Should().BeNull();
    }
    
    [Fact]
    public async Task Handle_Should_CallSaveChangesAsync()
    {
        // Arrange
        var context = Substitute.For<IApplicationDbContext>();
        var handler = new DeleteAssetCommandHandler(context);
        
        var asset = new Asset { Id = Guid.NewGuid() };
        
        context.Assets.FindAsync(Arg.Any<object?[]?>(), Arg.Any<CancellationToken>()).Returns(asset);

        var command = new DeleteAssetCommand(asset.Id);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        context.Assets.Received(1).Remove(asset);
        await context.Received(1).SaveChangesAsync(CancellationToken.None);
    }
}