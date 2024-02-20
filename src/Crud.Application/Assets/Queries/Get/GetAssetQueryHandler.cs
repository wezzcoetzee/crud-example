using Crud.Application.Common.Interfaces;
using Crud.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Crud.Application.Assets.Queries.Get;

public class GetAssetQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAssetQuery, Asset?>
{
    public async Task<Asset?> Handle(GetAssetQuery request, CancellationToken cancellationToken)
    {
        return await context.Assets.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
    }
}