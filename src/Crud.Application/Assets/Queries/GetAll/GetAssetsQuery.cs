using Crud.Domain.Entities;
using MediatR;

namespace Crud.Application.Assets.Queries.GetAll;

public record GetAssetsQuery() : IRequest<List<Asset>>;