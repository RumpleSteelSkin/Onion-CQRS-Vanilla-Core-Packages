using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace VCORE.Application.Features.Roles.Queries.GetAll;

public class RoleGetAllQueryHandling(RoleManager<IdentityRole<Guid>> roleManager)
    : IRequestHandler<RoleGetAllQuery, ICollection<IdentityRole<Guid>>>
{
    public async Task<ICollection<IdentityRole<Guid>>> Handle(RoleGetAllQuery request,
        CancellationToken cancellationToken)
    {
        return await roleManager.Roles.ToListAsync(cancellationToken);
    }
}