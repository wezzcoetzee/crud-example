using Crud.Domain.Enums;
using MediatR;

namespace Crud.Application.Assets.Commands.Update;

public abstract record UpdateAssetCommand(Guid Id, string Name, string Ticker, AssetClass Class) : IRequest;