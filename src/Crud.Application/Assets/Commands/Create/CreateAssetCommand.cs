using MediatR;

namespace Crud.Application.Assets.Commands.Create;

public record CreateAssetCommand(string Name, string Ticker) : IRequest<Guid>;