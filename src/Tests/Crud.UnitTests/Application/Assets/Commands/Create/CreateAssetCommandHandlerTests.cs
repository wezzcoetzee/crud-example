using Crud.Application.Assets.Commands.Create;
using Crud.Application.Common.Interfaces;
using Crud.Domain.Entities;
using NSubstitute;
using Xunit;

namespace Crud.UnitTests.Application.Assets.Commands.Create;

public class CreateAssetCommandHandlerTests
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly CreateAssetCommandHandler _handler;

    public CreateAssetCommandHandlerTests()
    {
        _context = Substitute.For<IApplicationDbContext>();
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
        _ = await _handler.Handle(command, default);

        // Assert
        await _context.Received().SaveChangesAsync(default);
        _context.Assets.Received().Add(Arg.Is<Asset>(x =>
            x.Name == command.Name &&
            x.Ticker == command.Ticker &&
            x.CreatedAt == utcNow &&
            x.UpdatedAt == utcNow
        ));
    }
}