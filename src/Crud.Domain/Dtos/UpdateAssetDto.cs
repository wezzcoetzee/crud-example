namespace Crud.Domain.Dtos;

public class UpdateAssetDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = "";
    public string Ticker { get; init; } = "";
}