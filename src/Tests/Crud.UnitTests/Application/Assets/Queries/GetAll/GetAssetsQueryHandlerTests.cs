using AutoFixture;
using Crud.Application.Assets.Queries.GetAll;
using Crud.Application.Common.Interfaces;
using Crud.Domain.Entities;
using Crud.Infrastructure.Data;
using Crud.UnitTests.Helpers;
using FluentAssertions;
using Xunit;

namespace Crud.UnitTests.Application.Assets.Queries.GetAll;

public class GetAssetsQueryHandlerTests : DatabaseHelper
{
    private readonly IFixture _fixture;
    private readonly IApplicationDbContext _context;
    private readonly GetAssetsQueryHandler _handler;

    public GetAssetsQueryHandlerTests()
    {
        _fixture = new Fixture();
        _context = new ApplicationDbContext(GetInMemoryDb);
        _handler = new GetAssetsQueryHandler(_context);
    }

    [Fact]
    public async Task Handle_ReturnsCorrectAssets()
    {
        // Arrange
        var assets = _fixture.CreateMany<Asset>().ToList();

        await _context.Assets.AddRangeAsync(assets);
        await _context.SaveChangesAsync(default);

        // Act
        var result = await _handler.Handle(new GetAssetsQuery(), new CancellationToken());

        // Assert
        assets.All(asset => result.Contains(asset)).Should().BeTrue();
    }
}