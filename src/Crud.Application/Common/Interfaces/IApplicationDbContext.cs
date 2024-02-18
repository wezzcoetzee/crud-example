using Crud.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crud.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Asset?> Assets { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}