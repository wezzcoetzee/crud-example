using Crud.Domain.Common;
using Crud.Domain.Enums;

namespace Crud.Domain.Entities;

public class Asset : BaseEntity
{
    public string Name { get; set; } = "";
    public string Ticker { get; set; } = "";
    public AssetClass Class { get; set; }
}