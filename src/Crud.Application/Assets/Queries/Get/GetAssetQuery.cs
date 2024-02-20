using Crud.Domain.Entities;
using MediatR;

namespace Crud.Application.Assets.Queries.Get;

public record GetAssetQuery(Guid Id) : IRequest<Asset?>;