using MediatR;
using Microsoft.AspNetCore.Mvc;
using VCORE.Application.Features.Roles.Commands.Create;
using VCORE.Application.Features.Roles.Queries.GetAll;

namespace VCORE.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IMediator mediator) : ControllerBase
    {
        [HttpPost("Add")]
        public async Task<IActionResult> Add(RoleAddCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await mediator.Send(new RoleGetAllQuery()));
        }
    }
}