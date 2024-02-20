using MediatR;

namespace Crud.Application.Assets.Commands.Update;

public record UpdateAssetCommand(Guid Id, string Name, string Ticker) : IRequest;