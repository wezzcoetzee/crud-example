using Xunit;

namespace Crud.IntegrationTests;

[CollectionDefinition(Name)]
public class CrudCollection : ICollectionFixture<CrudFactory>
{
    public const string Name = "Crud";
}