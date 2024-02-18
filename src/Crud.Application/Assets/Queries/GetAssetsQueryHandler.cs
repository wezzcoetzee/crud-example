using Crud.Application.Common.Interfaces;
using Crud.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Crud.Application.Assets.Queries;

public record GetAssetsQuery() : IRequest<List<Asset>>;

public class GetAssetsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAssetsQuery, List<Asset>>
{
    public async Task<List<Asset>> Handle(GetAssetsQuery request, CancellationToken cancellationToken)
    {
        return await context.Assets.ToListAsync(cancellationToken);
    }
}