using Crud.Application.Assets.Commands.Create;
using Crud.Application.Common.Interfaces;
using Crud.Domain.Entities;
using Crud.Infrastructure.Data;
using Crud.UnitTests.Helpers;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Crud.UnitTests.Application.Assets.Commands.Create;

public class CreateAssetCommandHandlerTests : DatabaseHelper
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly CreateAssetCommandHandler _handler;

    public CreateAssetCommandHandlerTests()
    {
        _context = new ApplicationDbContext(GetInMemoryDb);
        _dateTimeProvider = Substitute.For<IDateTimeProvider>();
        _handler = new CreateAssetCommandHandler(_context, _dateTimeProvider);
    }
    
    [Fact]
    public async Task Handle_ShouldCreateNewAsset()
    {
        // Arrange
        var command = new CreateAssetCommand("Bitcoin", "BTC");
        var utcNow = DateTime.UtcNow;
        _dateTimeProvider.UtcNow.Returns(utcNow);

        // Act
        var assetId = await _handler.Handle(command, default);

        // Assert
        _context.Assets.FirstOrDefault(f => f.Id == assetId).Should().NotBeNull();
    }

    [Fact]
    public async Task Handle_Should_CallSaveChangesAsync()
    {
        // Arrange
        var context = Substitute.For<IApplicationDbContext>();
        var handler = new CreateAssetCommandHandler(context, _dateTimeProvider);
        var command = new CreateAssetCommand("Bitcoin", "BTC");
        var utcNow = DateTime.UtcNow;
        _dateTimeProvider.UtcNow.Returns(utcNow);

        // Act
        _ = await handler.Handle(command, default);

        // Assert
        await context.Received().SaveChangesAsync(default);
        context.Assets.Received().Add(Arg.Is<Asset>(x =>
            x.Name == command.Name &&
            x.Ticker == command.Ticker &&
            x.CreatedAt == utcNow &&
            x.UpdatedAt == utcNow
        ));
    }
}