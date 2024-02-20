using Crud.Application.Assets.Queries.GetAll;
using Crud.Application.Common.Interfaces;
using Crud.Domain.Entities;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Crud.UnitTests.Application.Assets.Queries.GetAll;

public class GetAssetsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsCorrectAssets()
    {
        // Arrange
        var context = Substitute.For<IApplicationDbContext>();
        var assets = new List<Asset> { new(), new() };
        context.Assets.Returns(assets.AsQueryable());

        var handler = new GetAssetsQueryHandler(context);

        // Act
        var result = await handler.Handle(new GetAssetsQuery(), new CancellationToken());

        // Assert
        result.Should().BeEquivalentTo(assets);
    }
}