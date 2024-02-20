using Crud.Domain.Common;

namespace Crud.Domain.Entities;

public class Asset : BaseEntity
{
    public string Name { get; set; } = "";
    public string Ticker { get; set; } = "";
}