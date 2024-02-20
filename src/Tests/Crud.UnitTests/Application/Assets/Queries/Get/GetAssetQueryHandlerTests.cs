using Crud.Application.Assets.Queries.Get;
using Crud.Application.Common.Interfaces;
using Crud.Domain.Entities;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Crud.UnitTests.Application.Assets.Queries.Get;

public class GetAssetQueryHandlerTests
{
    private readonly IApplicationDbContext _context;
    private readonly GetAssetQueryHandler _handler;

    public GetAssetQueryHandlerTests()
    {
        _context = Substitute.For<IApplicationDbContext>();
        _handler = new GetAssetQueryHandler(_context);
    }

    [Fact]
    public async Task Handle_ValidQuery_ReturnsCorrectAsset()
    {
        // Arrange
        var asset = new Asset { Id = Guid.NewGuid() };
        var query = new GetAssetQuery(asset.Id);

        _context.Assets.Returns(new List<Asset> { asset }.AsQueryable());

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(asset);
    }
}