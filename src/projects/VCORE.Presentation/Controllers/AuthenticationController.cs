using MediatR;
using Microsoft.AspNetCore.Mvc;
using VCORE.Application.Features.Authentication.Commands.Login;
using VCORE.Application.Features.Authentication.Commands.Register;
using VCORE.Application.Features.Authentication.Queries.GetCurrentUser;

namespace VCORE.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IMediator mediator) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpGet("GetCurrentUser")]
        public IActionResult GetCurrentUser()
        {
            return Ok(mediator.Send(new GetCurrentUserQuery() { CurrentUser = User, CurrentHttpContext = HttpContext }));
        }
    }
}