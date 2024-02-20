using Crud.Domain.Enums;
using MediatR;

namespace Crud.Application.Assets.Commands.Create;

public abstract record CreateAssetCommand(string Name, string Ticker, AssetClass Class) : IRequest<Guid>;