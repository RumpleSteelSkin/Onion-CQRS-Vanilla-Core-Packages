using MediatR;
using Microsoft.AspNetCore.Identity;

namespace VCORE.Application.Features.Roles.Queries.GetAll;

public class RoleGetAllQuery : IRequest<ICollection<IdentityRole<Guid>>>;