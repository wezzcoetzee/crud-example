using MediatR;

namespace Crud.Application.Assets.Commands.Delete;

public record DeleteAssetCommand(Guid Id) : IRequest;