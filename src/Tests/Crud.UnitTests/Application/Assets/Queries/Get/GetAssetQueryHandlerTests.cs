using AutoFixture;
using Crud.Application.Assets.Queries.Get;
using Crud.Application.Common.Interfaces;
using Crud.Domain.Entities;
using Crud.Infrastructure.Data;
using Crud.UnitTests.Helpers;
using FluentAssertions;
using Xunit;

namespace Crud.UnitTests.Application.Assets.Queries.Get;

public class GetAssetQueryHandlerTests : DatabaseHelper
{
    private readonly IFixture _fixture;
    private readonly IApplicationDbContext _context;
    private readonly GetAssetQueryHandler _handler;

    public GetAssetQueryHandlerTests()
    {
        _fixture = new Fixture();
        _context = new ApplicationDbContext(GetInMemoryDb);
        _handler = new GetAssetQueryHandler(_context);
    }

    [Fact]
    public async Task Handle_ValidQuery_ReturnsCorrectAsset()
    {
        // Arrange
        var asset = _fixture.Create<Asset>();

        await _context.Assets.AddAsync(asset);
        await _context.SaveChangesAsync(default);
        
        var query = new GetAssetQuery(asset.Id);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(asset);
    }
}