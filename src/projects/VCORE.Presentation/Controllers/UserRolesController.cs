using MediatR;
using Microsoft.AspNetCore.Mvc;
using VCORE.Application.Features.UserRoles.Commands.Create;
using VCORE.Application.Features.UserRoles.Queries.GetByUserId;

namespace VCORE.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController(IMediator mediator) : ControllerBase
    {
        [HttpPost("Add")]
        public async Task<IActionResult> Add(UserRoleAddCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpGet("GetByUserId")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            return Ok(await mediator.Send(new UserRoleGetByUserIdQuery() { UserId = userId }));
        }
    }
}