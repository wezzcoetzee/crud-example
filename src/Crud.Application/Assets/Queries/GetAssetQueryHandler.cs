using Crud.Application.Common.Interfaces;
using Crud.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Crud.Application.Assets.Queries;

public record GetAssetQuery(Guid Id) : IRequest<Asset?>;

public class GetAssetQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAssetQuery, Asset?>
{
    public async Task<Asset?> Handle(GetAssetQuery request, CancellationToken cancellationToken)
    {
        return await context.Assets.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
    }
}

public class GetAssetQueryValidator : AbstractValidator<GetAssetQuery>
{
    public GetAssetQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}